using DemoCICD.Contract.Services.V1.Product;
using FluentValidation;

namespace DemoCICD.Contract.Services.V1.Product.Validators;
internal class DeletedProductValidator : AbstractValidator<Command.DeletedProductCommand>
{
    public DeletedProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is not empty");
    }
}
