namespace CrazyOrders.Domain.Events;

public interface IEvent
{
    string GetJsonPayload();
}
