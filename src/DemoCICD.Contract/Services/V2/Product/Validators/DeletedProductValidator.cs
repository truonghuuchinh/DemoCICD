using FluentValidation;

namespace DemoCICD.Contract.Services.V2.Product.Validators;
internal class DeletedProductValidator : AbstractValidator<Command.DeletedProductCommand>
{
    public DeletedProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is not empty");
    }
}
