namespace CrazyOrders.Domain.Entities.Orders
{
    public class OrderWithService : BaseOrder
    {
        public string[] Services { get; set; } = [];
    }

}
