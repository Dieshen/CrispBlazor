using CrispBlazor.Client.Modules.ProjectManagement.Models;
using CrispBlazor.Data;
using CrispBlazor.Modules.ProjectManagement.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrispBlazor.Modules.ProjectManagement.Configurations
{
    public sealed class ProjectConfiguration : BaseAuditableConfiguration<ProjectEntity>
    {
        public override void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(Project).Pluralize());

            builder.Property(e => e.Name)
                .IsRequired();
        }
    }
}
