using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Contract.Abstractions.Services.Product;
public static class Command
{
    public record CreatedProduct(string Name, decimal Price, string Description) : ICommand;

    public record UpdatedProduct(Guid Id, string Name, decimal Price, string Description) : ICommand;

    public record DeletedProduct(Guid Id): ICommand;
}
