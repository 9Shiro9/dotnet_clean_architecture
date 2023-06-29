namespace Domain.Entities
{
    public class Product : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        // Additional properties as needed

        public string SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
