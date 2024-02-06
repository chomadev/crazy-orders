namespace CrazyOrders.Domain.ValueObjects.Orders
{
    public class OrderWithProduct : BaseOrder
    {
        public string[] Products { get; set; } = [];
        

        public override bool IsValid()
        {
            return base.IsValid()
                && Products != null
                && Products.Length > 0;
        }
    }
}
