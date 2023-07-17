namespace Application.DTOs.SaleOrder
{
    public class SaleOrderDto
    {
        public SaleOrderDto(string orderNumber, DateTime orderDate, decimal totalPrice, int totalQuantity, string customerId, string customerName)
        {
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
            CustomerId = customerId;
            CustomerName = customerName;
        }

        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<SaleOrderItemDto> Items { get; set; } = new();       
    }

    public class SaleOrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public SaleOrderItemDto(string _productId, string _productName, int _quantity, decimal _unitPrice, decimal _totalPrice)
        {
            ProductId = _productId;
            ProductName = _productName;
            Quantity = _quantity;
            UnitPrice = _unitPrice;
            TotalPrice = _totalPrice;
        }
    }
}
