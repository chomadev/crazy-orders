namespace CrazyOrders.Domain.ValueObjects.Orders
{
    public class OrderWithService : BaseOrder
    {
        public string[] Services { get; set; } = [];
    }

}
