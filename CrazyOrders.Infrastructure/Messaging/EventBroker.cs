using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Infrastructure.Messaging
{
    public class EventBroker : IEventBroker
    {
        private readonly List<string> ConnectedClients = [];
        private readonly ILogger<EventBroker> logger;

        private readonly List<(long Offset, string Content)> MessagePool = [];
        private long MessageOffset = 0;

        public EventBroker(ILogger<EventBroker> logger)
        {
            this.logger = logger;
        }

        public string Connect()
        {
            var id = Guid.NewGuid().ToString();
            ConnectedClients.Add(id);
            logger.LogInformation("Client connected: {1}", id);
            return id;
        }

        public List<(long Offset, string Content)> Pull(string connectionId, long offset)
        {
            return MessagePool
                .Where(x => x.Offset > offset)
                .OrderBy(m => m.Offset)
                .Take(10)
                .ToList();
        }

        public void Push(IEvent e)
        {
            MessagePool.Add(new(MessageOffset++, e.GetJsonPayload()));
        }
    }
}
