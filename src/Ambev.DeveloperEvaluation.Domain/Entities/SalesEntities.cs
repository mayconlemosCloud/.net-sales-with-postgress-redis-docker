namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public bool IsCancelled { get; set; }

        public void AddItem(SaleItem item)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (item.Discount < 0 || item.Discount > item.Product.UnitPrice)
                throw new ArgumentException("Invalid discount value.");

            item.TotalAmount = (item.Product.UnitPrice - item.Discount) * item.Quantity;
            Items.Add(item);
            UpdateTotalAmount();
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = Items.Sum(i => i.TotalAmount);
        }
    }
}