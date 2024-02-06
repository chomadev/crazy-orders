namespace CrazyOrders.Domain.ValueObjects
{
    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }

    }
}
