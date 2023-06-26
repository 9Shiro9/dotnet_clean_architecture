using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseAuditEntity
    {
        public string ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Product() { ProductId = Guid.NewGuid().ToString(); }
        public Product(string _code, string _description, decimal _price, string _categoryId)
        {
            ProductId = Guid.NewGuid().ToString();
            Code = _code;
            Description = _description;
            Price = _price;
            CategoryId = _categoryId;
        }
    }
}
