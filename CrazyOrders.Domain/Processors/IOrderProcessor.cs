using CrazyOrders.Domain.ValueObjects.Orders;

namespace CrazyOrders.Domain.Processors
{
    public interface IOrderProcessor
    {
        public void ProcessOrder(BaseOrder order);
    }
}
