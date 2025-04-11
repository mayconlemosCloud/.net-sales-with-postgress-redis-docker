using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Handler for processing CreateSalesCommand
/// </summary>
public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly IMapper _mapper;

    public CreateSalesHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand request, CancellationToken cancellationToken)
    {
        // Logic to handle the creation of a sale
        return new CreateSalesResult
        {
            SaleId = Guid.NewGuid(),
            ProductName = request.ProductName,
            Quantity = request.Quantity,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow
        };
    }
}