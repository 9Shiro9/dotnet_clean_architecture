namespace Domain.Entities
{
    public class SaleOrder : BaseAuditEntity
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }

        public SaleOrder()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;

        }

        public SaleOrder(string orderNumber, DateTime orderDate, decimal totalPrice, int totalQuantity, string customerId)
        {
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
            CustomerId = customerId;
        }
    }
}
