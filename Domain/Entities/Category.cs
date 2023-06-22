using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; }
        public string Description { get; set; }
        public Category(string _code, string _description)
        {
            Code = _code;
            Description = _description;
        }
    }
}
