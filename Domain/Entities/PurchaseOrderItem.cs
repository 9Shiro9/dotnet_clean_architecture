namespace Domain.Entities
{

    public class PurchaseOrderItem : BaseAuditEntity
    {
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }

        public PurchaseOrderItem(string purchaseOrderId, string productId, int quantity, decimal unitPrice, decimal totalPrice)
        {
            Id = Guid.NewGuid().ToString();
            PurchaseOrderId = purchaseOrderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }
    }

}
