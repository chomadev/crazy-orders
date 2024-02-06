using CrazyOrders.Domain.Processors;
using CrazyOrders.Domain.ValueObjects.Orders;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Application.Processors
{
    public class OrderProcessorFactory
    {
        public static IOrderProcessor GetOrderProcessor(ILogger logger, BaseOrder order)
        {
            if (order is OrderWithProduct)
                return (IOrderProcessor) new OrderWithProductProcessor(logger);
            else if (order is OrderWithService)
                return (IOrderProcessor) new OrderWithServiceProcessor(logger);

            throw new NotImplementedException();
        }
    }
}
