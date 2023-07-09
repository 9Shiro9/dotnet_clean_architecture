namespace Application.DTOs.PurchaseOrder
{
    public class PurchaseOrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new();

        public PurchaseOrderDto(string orderNumber, DateTime orderDate, decimal totalPrice, int totalQuantity)
        {
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
        }
    }

    public class PurchaseOrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public PurchaseOrderItemDto(string _productId, string _productName, int _quantity, decimal _unitPrice, decimal _totalPrice)
        {
            ProductId = _productId;
            ProductName = _productName;
            Quantity = _quantity;
            UnitPrice = _unitPrice;
            TotalPrice = _totalPrice;
        }
    }
}
