namespace Domain.Entities
{
    public class Review : BaseAuditEntity
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
