namespace Domain.Entities
{
    public class SaleOrderItem : BaseAuditEntity
    {
        public SaleOrderItem()
        {

        }
        public SaleOrderItem(string orderId, string productId, int quantity, decimal unitPrice, decimal totalPrice)
        {
            Id = Guid.NewGuid().ToString();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }

        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual SaleOrder SaleOrder { get; set; }
        public virtual Product Product { get; set; }

    }

}
