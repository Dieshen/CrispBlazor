using CrispBlazor.Shared.Events;

namespace CrispBlazor.Shared.Interfaces
{
    public interface IEventService
    {
        Task Publish(BaseEvent @event);
    }
}
