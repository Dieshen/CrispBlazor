namespace CrispBlazor.Data
{
    public sealed record StoredFileObject : ValueObject
    {
        public required string Key { get; set; }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}
