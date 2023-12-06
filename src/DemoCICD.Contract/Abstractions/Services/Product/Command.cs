using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Contract.Abstractions.Services.Product;
public sealed class Command
{
    public sealed record CreatedProductCommand(string Name, decimal Price, string Description)
        : ICommand;

    public sealed record UpdatedProductCommand : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }

    public sealed record DeletedProductCommand(Guid Id)
        : ICommand;
}
