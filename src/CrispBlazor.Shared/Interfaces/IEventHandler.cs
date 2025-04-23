using CrispBlazor.Shared.Events;

namespace CrispBlazor.Shared.Interfaces
{
    public interface IEventHandler<TEvent> where TEvent : BaseEvent
    {
        ValueTask Handle(TEvent @event);
    }
}
