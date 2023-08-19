namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductId = Guid.NewGuid().ToString();
        }
        public Product(string code, string description, string categoryId, decimal buyingPrice, decimal sellingPrice, int quantity)
        {
            ProductId = Guid.NewGuid().ToString();
            Code = code;
            Description = description;
            CategoryId = categoryId;
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Quantity = quantity;
        }

        public string ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
    }
}
