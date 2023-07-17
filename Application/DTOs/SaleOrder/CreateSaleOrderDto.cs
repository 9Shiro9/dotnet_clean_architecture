namespace Application.DTOs.SaleOrder
{
    public class CreateSaleOrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string CustomerId { get; set; }
        public List<CreateSaleOrderItemDto> Items { get; set; }

        public CreateSaleOrderDto(string orderNumber,string customerId, List<CreateSaleOrderItemDto> items)
        {
            OrderDate = DateTime.Now;
            OrderNumber = orderNumber;
            CustomerId = customerId;
            Items = items;
            TotalPrice= Items.Sum(x => x.TotalPrice);
            TotalQuantity = items.Sum(x => x.Quantity);
        }

    }

    public class CreateSaleOrderItemDto
    {
        public string SaleOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public CreateSaleOrderItemDto() { }
        public CreateSaleOrderItemDto(string _productId, int _quantity, decimal _unitPrice, decimal _totalPrice)
        {
            ProductId = _productId;
            Quantity = _quantity;
            UnitPrice = _unitPrice;
            TotalPrice = _totalPrice;
        }
    }
}
