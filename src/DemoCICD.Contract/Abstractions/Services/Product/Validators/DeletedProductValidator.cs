using FluentValidation;

namespace DemoCICD.Contract.Abstractions.Services.Product.Validators;
internal class DeletedProductValidator : AbstractValidator<Command.DeletedProductCommand>
{
    public DeletedProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is not empty");
    }
}
