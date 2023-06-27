namespace Domain.Entities
{
    public class CartItem : BaseAuditEntity
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
