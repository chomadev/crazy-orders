namespace CrazyOrders.Domain.ValueObjects.Orders
{
    public class BaseOrder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public PaymentDetail? PaymentDetail { get; set; }
    }
}
