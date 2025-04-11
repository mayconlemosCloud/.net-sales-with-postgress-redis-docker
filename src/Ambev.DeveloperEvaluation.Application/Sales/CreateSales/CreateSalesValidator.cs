using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Validator for CreateSalesCommand
/// </summary>
public class CreateSalesValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesValidator()
    {

        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required.");
    }
}