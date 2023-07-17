namespace Application.DTOs.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
    }
}
