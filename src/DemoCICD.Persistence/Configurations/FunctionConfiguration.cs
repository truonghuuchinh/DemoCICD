using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCICD.Persistence.Configurations;
internal sealed class FunctionConfiguration : IEntityTypeConfiguration<Function>
{
    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.ToTable(TableNames.Functions);

        builder.HasKey(f => f.Id);

        builder
            .HasMany(x => x.Permissions)
            .WithOne()
            .HasForeignKey(p => p.FunctionId);

        builder
           .HasMany(x => x.ActionInFunctions)
           .WithOne()
           .HasForeignKey(p => p.FunctionId);
    }
}
