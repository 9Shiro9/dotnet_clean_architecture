namespace Domain.Entities
{
    public class Category : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        public Category() { Id = Guid.NewGuid().ToString(); }
        public Category(string categoryName)
        {
            Id = Guid.NewGuid().ToString();
            Name = categoryName;
        }
    }
}
