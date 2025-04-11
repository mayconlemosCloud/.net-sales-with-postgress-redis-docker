using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Command for creating a sale
/// </summary>
public class CreateSalesCommand : IRequest<CreateSalesResult>
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}