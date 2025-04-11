namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Result of creating a sale
/// </summary>
public class CreateSalesResult
{
    public Guid SaleId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}