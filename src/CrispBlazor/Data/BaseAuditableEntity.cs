namespace CrispBlazor.Data
{
    public abstract record BaseAuditableEntity : BaseEntity, IAuditable
    {
        public BaseAuditableEntity(string createdBy) : base()
        {
            LastModifiedBy = CreatedBy = createdBy;
            LastModified = Created = DateTimeOffset.UtcNow;
        }

        protected BaseAuditableEntity()
        {
        }

        public DateTimeOffset Created { get; init; }
        public required string CreatedBy { get; init; }
        public DateTimeOffset LastModified { get; init; }
        public required string LastModifiedBy { get; init; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
    }
}
