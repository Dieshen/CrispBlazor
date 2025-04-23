namespace CrispBlazor.Data
{
    [Serializable]
    public abstract record ValueObject : IComparable, IComparable<ValueObject>
    {
        private int? _cachedHashCode;

        protected abstract IEnumerable<IComparable> GetEqualityComponents();

        public override int GetHashCode()
        {
            if (!_cachedHashCode.HasValue)
            {
                _cachedHashCode = GetEqualityComponents().Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
            }

            return _cachedHashCode.Value;
        }

        public virtual int CompareTo(ValueObject? other)
        {
            if (other is null)
            {
                return 1;
            }

            if ((object)this == other)
            {
                return 0;
            }

            Type unproxiedType = GetUnproxiedType(this);
            Type unproxiedType2 = GetUnproxiedType(other);
            if (unproxiedType != unproxiedType2)
            {
                return string.Compare($"{unproxiedType}", $"{unproxiedType2}", StringComparison.Ordinal);
            }

            return GetEqualityComponents().Zip(other.GetEqualityComponents(), (left, right) => left?.CompareTo(right) ?? (right != null ? -1 : 0)).FirstOrDefault((cmp) => cmp != 0);
        }

        public virtual int CompareTo(object? other)
        {
            return CompareTo(other as ValueObject);
        }

        internal static Type GetUnproxiedType(object obj)
        {
            Type type = obj.GetType();
            string text = type.ToString();
            if (text.Contains("Castle.Proxies.") || text.EndsWith("Proxy"))
            {
#pragma warning disable CS8603 // Type should have a base type.
                return type.BaseType;
#pragma warning restore CS8603 
            }

            return type;
        }
    }
}
