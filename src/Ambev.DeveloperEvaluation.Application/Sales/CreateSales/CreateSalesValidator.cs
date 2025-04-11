using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Validator for CreateSalesCommand
/// </summary>
public class CreateSalesValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}