namespace Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }

        public CreateProductDto(string code, string description, decimal buyingPrice, decimal sellingPrice, int quantity)
        {
            Code = code;
            Description = description;
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Quantity = quantity;
        }
    }
}
