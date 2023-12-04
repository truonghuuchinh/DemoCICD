using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCICD.Persistence.Configurations;
internal sealed class ActionInFunctionConfiguration : IEntityTypeConfiguration<ActionInFunction>
{
    public void Configure(EntityTypeBuilder<ActionInFunction> builder)
    {
        builder.ToTable(TableNames.ActionInFunctionns);

        builder.HasKey(x => new { x.FunctionId, x.ActionId });
    }
}
