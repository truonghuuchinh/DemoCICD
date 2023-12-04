using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCICD.Persistence.Configurations;
internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(TableNames.AppRoles);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Claims)
            .WithOne()
            .HasForeignKey(a => a.RoleId);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne()
            .HasForeignKey(a => a.RoleId);

        builder
            .HasMany(x => x.Permissions)
            .WithOne()
            .HasForeignKey(a => a.RoleId);
    }
}
