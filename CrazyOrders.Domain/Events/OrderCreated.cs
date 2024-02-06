namespace CrazyOrders.Domain.Events
{
    public class OrderCreated(string jsonPayload) : IEvent
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        private string JsonPayload { get; set; } = jsonPayload;

        public string GetJsonPayload()
        {
            return JsonPayload;
        }
    }
}
