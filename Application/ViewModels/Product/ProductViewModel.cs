namespace Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set;}
    }
}
