using FluentValidation;

namespace DemoCICD.Contract.Abstractions.Services.Product.Validators;
internal class DeletedProductValidator : AbstractValidator<Command.DeletedProduct>
{
    public DeletedProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is not empty");
    }
}
