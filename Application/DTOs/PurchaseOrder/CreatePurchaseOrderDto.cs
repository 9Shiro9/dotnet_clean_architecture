namespace Application.DTOs.PurchaseOrder
{
    public class CreatePurchaseOrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public List<CreatePurchaseOrderItemDto> Items { get; set; } 

        public CreatePurchaseOrderDto(string orderNumber, List<CreatePurchaseOrderItemDto> items)
        {
            OrderDate = DateTime.Now;
            OrderNumber = orderNumber;
            Items = items;
            TotalPrice= Items.Sum(x => x.TotalPrice);
            TotalQuantity = items.Sum(x => x.Quantity);
        }

    }

    public class CreatePurchaseOrderItemDto
    {
        public string PurchaseOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public CreatePurchaseOrderItemDto() { }
        public CreatePurchaseOrderItemDto(string _productId, int _quantity, decimal _unitPrice, decimal _totalPrice)
        {
            ProductId = _productId;
            Quantity = _quantity;
            UnitPrice = _unitPrice;
            TotalPrice = _totalPrice;
        }
    }
}
