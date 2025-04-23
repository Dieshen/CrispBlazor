namespace CrispBlazor.Shared.Requests
{
    public sealed record FileRequest
    {
        public string Key { get; init; } = default!;
    }
}
