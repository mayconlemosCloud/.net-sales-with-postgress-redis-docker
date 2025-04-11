namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Result of retrieving a sale
/// </summary>
public class GetSalesResult
{
    public Guid SaleId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}