using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Contract.Services.V2.Product;
public sealed class Command
{
    public sealed record CreatedProductCommand(
        string Name,
        decimal Price,
        string Description)
        : ICommand;

    public sealed record UpdatedProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price)
        : ICommand;

    public sealed record DeletedProductCommand(Guid Id)
        : ICommand;
}
