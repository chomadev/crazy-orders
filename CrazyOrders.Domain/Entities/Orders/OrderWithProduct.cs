namespace CrazyOrders.Domain.Entities.Orders
{
    public class OrderWithProduct : BaseOrder
    {
        public string[] Products { get; set; } = [];
    }
}
