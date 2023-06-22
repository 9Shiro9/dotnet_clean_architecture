using Domain.Common;

namespace Domain.Entities
{
    public class OrderItem : BaseAuditEntity
    {
        public string OrderItemId { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderItem(string _orderId, string _productId, decimal _price, int _quantity)
        {
            OrderItemId = Guid.NewGuid().ToString();
            OrderId = _orderId;
            ProductId = _productId;
            Price = _price;
            Quantity = _quantity;
            TotalPrice = Price * Quantity;
        }
    }
}
