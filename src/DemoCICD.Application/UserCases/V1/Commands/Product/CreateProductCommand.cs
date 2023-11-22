using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommand : ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }
}
