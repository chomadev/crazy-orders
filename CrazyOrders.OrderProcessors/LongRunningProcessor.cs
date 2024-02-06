using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Processors;
using CrazyOrders.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;


namespace CrazyOrders.OrderProcessors
{
    public class LongRunningProcessor : BackgroundService
    {
        public void Run(IEventBroker eventBroker, ILogger<LongRunningProcessor> logger)
        {
            var connectionId = eventBroker.Connect();
            long offset = 0;

            do
            {
                var ordersToProcess = eventBroker.Pull(connectionId, offset).OrderBy(x => x.Offset);

                foreach (var orderMessage in ordersToProcess)
                {
                    logger.LogInformation("Processing order {1}", orderMessage.Offset);
                    try
                    {
                        var order = JsonConvert.DeserializeObject<Order>(orderMessage.Content);
                        new OrderWithProductProcessor(logger).ProcessOrder(order);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("ERROR Processing order {1}", JsonConvert.SerializeObject(orderMessage));
                        throw;
                    }
                    offset = orderMessage.Offset;
                }
                Thread.Sleep(3000);
            } while (true);

        }
    }
}
