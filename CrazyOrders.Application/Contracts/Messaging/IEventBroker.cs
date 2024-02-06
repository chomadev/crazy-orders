using CrazyOrders.Domain.Events;

namespace CrazyOrders.Application.Contracts.Messaging
{
    public interface IEventBroker
    {
        void Push(IEvent e);

        List<(long Offset, string Content)> Pull(string connectionId, long offset);

        string Connect();
    }
}
