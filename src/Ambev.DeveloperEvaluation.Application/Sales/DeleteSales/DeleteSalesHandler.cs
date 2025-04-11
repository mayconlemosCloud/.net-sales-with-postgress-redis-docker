using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

/// <summary>
/// Handler for processing DeleteSalesCommand
/// </summary>
public class DeleteSalesHandler : IRequestHandler<DeleteSalesCommand, DeleteSalesResponse>
{
    public async Task<DeleteSalesResponse> Handle(DeleteSalesCommand request, CancellationToken cancellationToken)
    {
        // Logic to handle the deletion of a sale
        return new DeleteSalesResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        };
    }
}