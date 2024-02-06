using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Processors;
using CrazyOrders.Domain.Processors;
using CrazyOrders.Domain.ValueObjects.Orders;
using Newtonsoft.Json;

namespace CrazyOrders.API.OrderProcessors
{
    public class LongRunningOrderProcessor : IHostedService
    {
        private readonly IEventBroker eventBroker;
        private readonly ILogger<LongRunningOrderProcessor> logger;
        private Timer? timer = null;
        private string connectionId;
        private long offset = 0;

        public LongRunningOrderProcessor(IEventBroker eventBroker, ILogger<LongRunningOrderProcessor> logger)
        {
            this.eventBroker = eventBroker;
            this.logger = logger;
            connectionId = eventBroker.Connect();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("LongRunningProcessor started");
            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(2));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Hosted Service is stopping.");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        protected void DoWork(object? state)
        {
            var ordersToProcess = eventBroker.Pull(connectionId, offset).OrderBy(x => x.Offset);

            foreach (var orderMessage in ordersToProcess)
            {
                logger.LogInformation("Processing order {1}", orderMessage.Offset);
                try
                {
                    var order = JsonConvert.DeserializeObject<BaseOrder>(orderMessage.Content);
                    var orderType = order.GetType();

                    var orderProcessor = OrderProcessorFactory.GetOrderProcessor(logger, order);

                    orderProcessor.ProcessOrder((OrderWithProduct)order);
                }
                catch (Exception ex)
                {
                    logger.LogError("ERROR Processing order {1}", JsonConvert.SerializeObject(orderMessage));
                    throw;
                }
                offset = orderMessage.Offset;
            }
        }
    }
}
