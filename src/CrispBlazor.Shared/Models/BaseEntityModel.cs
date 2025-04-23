namespace CrispBlazor.Shared.Models
{
    public abstract record BaseEntityModel : BaseModel
    {
        public Guid Id { get; init; }
    }
}
