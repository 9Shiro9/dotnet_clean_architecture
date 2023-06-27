namespace Domain.Entities
{
    public class Product : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Variant> Variants { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
