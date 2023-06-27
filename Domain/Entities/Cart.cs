using Domain.Common;

namespace Domain.Entities
{
    public class Cart : BaseAuditEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
