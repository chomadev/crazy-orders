using CrazyOrders.Domain.Processors;
using CrazyOrders.Domain.ValueObjects.Orders;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Application.Processors
{
    public class OrderWithServiceProcessor : IOrderProcessor
    {
        private readonly ILogger logger;

        public OrderWithServiceProcessor(ILogger logger)
        {
            this.logger = logger;
        }

        public void ProcessOrder(BaseOrder order)
        {
            logger.LogInformation("Order Processor With Service - activating services for order {1}", order.Id);
        }
    }
}
