namespace CrispBlazor.Shared.Requests
{
    public sealed record FileUploadRequest
    {
        public string Name { get; init; } = default!;
        public string Root { get; init; } = ".temp";
        public string ContentType { get; init; } = "multipart/form-data";
        public bool NeedsGuid { get; init; } = true;
    }
}
