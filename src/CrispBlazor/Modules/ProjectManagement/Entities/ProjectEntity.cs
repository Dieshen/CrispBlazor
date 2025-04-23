using CrispBlazor.Data;

namespace CrispBlazor.Modules.ProjectManagement.Entities
{
    public sealed record ProjectEntity : BaseAuditableEntity
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
