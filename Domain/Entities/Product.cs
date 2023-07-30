namespace Domain.Entities
{
    public class Product : BaseAuditEntity
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

        public Product(string _code,string _description,decimal _buyingPrice,decimal _sellingPrice,int _quantity)
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Code = _code;
            Description = _description;
            BuyingPrice = _buyingPrice;
            SellingPrice = _sellingPrice;
            Quantity = _quantity;
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
    }
}
