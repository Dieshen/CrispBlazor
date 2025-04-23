namespace CrispBlazor.Shared.Events
{
    public abstract record BaseEvent
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}
