namespace Domain.Entities
{
    public class Order : BaseAuditEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
