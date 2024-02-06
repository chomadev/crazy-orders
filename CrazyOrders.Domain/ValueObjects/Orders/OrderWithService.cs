namespace CrazyOrders.Domain.ValueObjects.Orders
{
    public class OrderWithService : BaseOrder
    {
        public string[] Services { get; set; } = [];

        public override bool IsValid()
        {
            return base.IsValid()
                && Services.Length > 0;
        }
    }

}
