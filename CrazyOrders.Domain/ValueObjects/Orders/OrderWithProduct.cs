namespace CrazyOrders.Domain.ValueObjects.Orders
{
    public class OrderWithProduct : BaseOrder
    {
        public string[] Products { get; set; } = [];
    }
}
