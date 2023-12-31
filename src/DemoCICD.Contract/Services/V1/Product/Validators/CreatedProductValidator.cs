﻿using DemoCICD.Contract.Services.V1.Product;
using FluentValidation;

namespace DemoCICD.Contract.Services.V1.Product.Validators;
public class CreatedProductValidator : AbstractValidator<Command.CreatedProductCommand>
{
    public CreatedProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is not empty");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is not empty");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be grater than 0");
    }
}
