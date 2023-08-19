namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryId { get; set; }
        public string Code { get; set; }
        public string Descripton { get; set; }
        public virtual ICollection<Product> Products { get; set;}
    }
}
