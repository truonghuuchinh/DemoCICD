using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCICD.Persistence.Configurations;
internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(TableNames.AppUsers);

        builder.Property(x => x.FisrtName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.IsDirector).HasDefaultValue(false);
        builder.Property(x => x.IsHeadOfDepartment).HasDefaultValue(false);

        builder
            .HasMany(x => x.Claims)
            .WithOne()
            .HasForeignKey(c => c.UserId);

        builder
            .HasMany(x => x.Logins)
            .WithOne()
            .HasForeignKey(c => c.UserId);

        builder
            .HasMany(x => x.Tokens)
            .WithOne()
            .HasForeignKey(c => c.UserId);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne()
            .HasForeignKey(c => c.UserId);
    }
}
