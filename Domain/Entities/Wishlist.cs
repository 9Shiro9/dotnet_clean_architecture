namespace Domain.Entities
{
    public class Wishlist : BaseAuditEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
