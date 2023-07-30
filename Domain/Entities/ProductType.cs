namespace Domain.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Descripton { get; set; }
        public virtual ICollection<Product> Products { get; set;}
    }
}
