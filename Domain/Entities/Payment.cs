namespace Domain.Entities
{
    public class Payment : BaseAuditEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
