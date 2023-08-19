namespace Domain.Entities
{
    public class SaleOrderItem : BaseEntity
    {
        public SaleOrderItem()
        {
            SaleOrderItemId = Guid.NewGuid().ToString();
        }

        public SaleOrderItem(string saleOrderId, string productId, int quantity, decimal unitPrice)
        {
            SaleOrderItemId = Guid.NewGuid().ToString();
            SaleOrderId = saleOrderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Total = quantity * unitPrice;
        }

        public string SaleOrderItemId { get; set; }
        public string SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

}
