namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}