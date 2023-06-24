using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseAuditEntity
    {
        public string CategoryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public Category(string _code, string _description)
        {
            CategoryId = Guid.NewGuid().ToString();
            Code = _code;
            Description = _description;
        }
    }
}
