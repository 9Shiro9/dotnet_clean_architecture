namespace Domain.Entities
{
    public class ProductCategory : BaseAuditEntity
    {
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
