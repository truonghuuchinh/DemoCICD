using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistence.Configurations;
public sealed class ActionConfiguration : IEntityTypeConfiguration<Action>
{
    public void Configure(EntityTypeBuilder<Action> builder)
    {
        builder.ToTable(TableNames.Actions);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasMaxLength(50);
        builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
        builder.Property(a => a.IsActive).HasDefaultValue(true);
        builder.Property(a => a.SortOrder).HasDefaultValue(null);

        builder
            .HasMany(a => a.ActionInFunctions)
            .WithOne()
            .HasForeignKey(p => p.ActionId);

        builder
            .HasMany(a => a.Permissions)
            .WithOne()
            .HasForeignKey(p => p.ActionId);
    }
}
