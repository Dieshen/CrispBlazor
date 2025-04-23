using CrispBlazor.Shared.Interfaces;

namespace CrispBlazor.Shared.Requests
{
    public abstract record Command<TResponse> : IRequest<TResponse>;
    public abstract record Command : IRequest;
    public abstract record CreateCommand : Command<Guid>;
    public abstract record ModifyCommand : Command
    {
        public ModifyCommand(Guid Id) => this.Id = Id;
        public ModifyCommand() { }
        public Guid Id { get; init; }
    }
    public abstract record DeleteCommand : ModifyCommand
    {
        public DeleteCommand(Guid Id) : base(Id) { }
        public DeleteCommand() { }
    }
    public abstract record ArchiveCommand : ModifyCommand
    {
        public ArchiveCommand(Guid Id) : base(Id) { }
        public ArchiveCommand() { }
    }
}
