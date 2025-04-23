using CrispBlazor.Shared.Models;

namespace CrispBlazor.Client.Modules.ProjectManagement.Models
{
    public sealed record Project : BaseAuditableModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
