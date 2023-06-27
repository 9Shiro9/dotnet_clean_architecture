namespace Domain.Entities
{
    public class Variant : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
