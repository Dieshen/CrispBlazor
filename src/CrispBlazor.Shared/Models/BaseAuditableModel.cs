namespace CrispBlazor.Shared.Models
{
    public abstract record BaseAuditableModel : BaseEntityModel
    {
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public string CreatedBy { get; init; } = default!;
        public DateTimeOffset LastModified { get; init; } = DateTimeOffset.UtcNow;
        public string LastModifiedBy { get; init; } = default!;
        public bool IsArchived { get; init; }
        public bool IsDeleted { get; init; }
    }
}
