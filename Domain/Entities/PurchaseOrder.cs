namespace Domain.Entities
{
    public class PurchaseOrder : BaseAuditEntity
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        // Additional properties as needed
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        public PurchaseOrder(string orderNumber)
        {
            Id = Guid.NewGuid().ToString();
            OrderNumber = orderNumber;
            OrderDate = DateTime.Now;
        }
        public PurchaseOrder(string orderNumber, decimal totalPrice, int totalQuantity)
        {
            Id = Guid.NewGuid().ToString();
            OrderNumber = orderNumber;
            OrderDate = DateTime.Now;
            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
        }
    }
}
