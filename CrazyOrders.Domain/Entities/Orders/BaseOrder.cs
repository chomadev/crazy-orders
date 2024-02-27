using CrazyOrders.Domain.ValueObjects;

namespace CrazyOrders.Domain.Entities.Orders
{
    public class BaseOrder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public PaymentDetail? PaymentDetail { get; set; }
    }
}
