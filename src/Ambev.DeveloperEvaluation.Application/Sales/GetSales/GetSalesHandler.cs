using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Handler for processing GetSalesCommand
/// </summary>
public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
{
    public Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        // Logic to handle the retrieval of a sale
        return Task.FromResult(new GetSalesResult
        {
            SaleId = request.SaleId,
            ProductName = "Sample Product",
            Quantity = 10,
            Price = 100.0m,
            CreatedAt = DateTime.UtcNow
        });
    }
}