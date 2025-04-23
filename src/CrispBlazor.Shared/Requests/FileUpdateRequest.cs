namespace CrispBlazor.Shared.Requests
{
    public sealed record FileUpdateRequest
    {
        public string From { get; init; } = default!;
        public string To { get; init; } = default!;
    }
}
