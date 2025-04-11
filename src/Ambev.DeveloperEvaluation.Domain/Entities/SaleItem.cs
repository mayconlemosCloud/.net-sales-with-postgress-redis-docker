namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal UnitPrice => Product?.UnitPrice ?? 0;
    }
}