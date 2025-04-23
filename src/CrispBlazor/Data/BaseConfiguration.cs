using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrispBlazor.Data
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }

    public abstract class BaseAuditableConfiguration<T> : BaseConfiguration<T>
        where T : BaseAuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.CreatedBy)
                .IsRequired();

            builder.Property(e => e.Created)
                .IsRequired();

            builder.Property(e => e.LastModifiedBy)
                .IsRequired();

            builder.Property(e => e.LastModified)
                .IsRequired();

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(e => e.IsArchived)
                .HasDefaultValue(false);
        }
    }
}
