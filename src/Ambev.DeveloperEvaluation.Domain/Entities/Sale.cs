namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public bool IsCancelled { get; set; }

        public Sale()
        {
            Id = Guid.NewGuid();
        }



    }
}