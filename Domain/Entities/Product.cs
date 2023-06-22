using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseAuditEntity
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Product(string _code, string _description, decimal _price, string _categoryId)
        {
            Code = _code;
            Description = _description;
            Price = _price;
            CategoryId = _categoryId;
        }
    }
}
