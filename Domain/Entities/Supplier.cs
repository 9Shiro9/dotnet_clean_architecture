namespace Domain.Entities
{
    public class Supplier : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // Additional properties as needed

        public virtual ICollection<Product> Products { get; set; }
    }
}
