using CrazyOrders.Domain.Entities.Orders;

namespace CrazyOrders.Domain.Processors
{
    public interface IOrderProcessor
    {
        public void ProcessOrder(BaseOrder order);
    }
}
