namespace CrispBlazor.Shared.Responses
{
    public sealed record FileResponse
    {
        public string URL { get; init; } = default!;
        public string Key { get; init; } = default!;
    }
}
