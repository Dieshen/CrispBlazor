using CrispBlazor.Shared.Requests;

namespace CrispBlazor.Client.Modules.ProjectManagement.Requests
{
    public sealed record CreateProject : CreateCommand
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }

    public sealed record UpdateProject : ModifyCommand
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }

    public sealed record DeleteProject : DeleteCommand;
    public sealed record ArchiveProject : ArchiveCommand;
}