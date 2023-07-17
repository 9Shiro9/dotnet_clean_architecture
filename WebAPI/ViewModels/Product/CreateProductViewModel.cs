namespace WebAPI.ViewModels.Product
{
    public class CreateProductViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
    }
}
