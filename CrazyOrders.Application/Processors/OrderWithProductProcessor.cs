using CrazyOrders.Domain.Processors;
using CrazyOrders.Domain.ValueObjects.Orders;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Application.Processors
{
    public class OrderWithProductProcessor : IOrderProcessor
    {
        private readonly ILogger logger;

        public OrderWithProductProcessor(ILogger logger)
        {
            this.logger = logger;
        }

        public void ProcessOrder(BaseOrder order)
        {
            logger.LogInformation("Order Processor With Product - initiating delivery for order {1}", order.Id);
        }
    }
}
