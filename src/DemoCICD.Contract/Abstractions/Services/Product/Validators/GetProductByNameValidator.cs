using FluentValidation;

namespace DemoCICD.Contract.Abstractions.Services.Product.Validators;
public class GetProductByNameValidator : AbstractValidator<Query.GetProductByNameQuery>
{
    public GetProductByNameValidator()
    {
        RuleFor(p => p.Name)
            .NotEqual("Chinh")
            .WithMessage("Name can't be empty");
    }
}
