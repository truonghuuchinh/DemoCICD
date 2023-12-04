using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Contract.Abstractions.Services.Product;
public sealed class Command
{
    public record CreatedProduct(string Name, decimal Price, string Description) : ICommand;

    public record UpdatedProduct : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
    public record DeletedProduct(Guid Id) : ICommand;
}
