namespace CrispBlazor.Data
{
    public abstract record BaseEntity : Entity<int>
    {
        protected BaseEntity()
        {

        }

        protected BaseEntity(int id)
        {
            Id = id;
        }
    }

    public abstract record Entity<TId> : IComparable, IComparable<Entity<TId>>
        where TId : IComparable<TId>
    {
        public virtual TId Id { get; set; }

#pragma warning disable CS8618 // This is needed for the ORM for serializing Value Objects
        protected Entity()
#pragma warning restore CS8618 
        {
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return (ValueObject.GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }

        public virtual int CompareTo(Entity<TId>? other)
        {
            if (other is null)
            {
                return 1;
            }

            if ((object)this == other)
            {
                return 0;
            }

            return Id.CompareTo(other.Id);
        }

        public virtual int CompareTo(object? other)
        {
            return CompareTo(other as Entity<TId>);
        }
    }
}
