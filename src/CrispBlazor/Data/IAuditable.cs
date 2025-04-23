namespace CrispBlazor.Data
{
    public interface IAuditable
    {
        DateTimeOffset Created { get; }
        string CreatedBy { get; }
        DateTimeOffset LastModified { get; }
        string LastModifiedBy { get; }
        bool IsDeleted { get; }
        bool IsArchived { get; }
    }
}
