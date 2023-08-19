namespace Domain.Entities
{
    public class SaleOrder : BaseEntity
    {
        public SaleOrder()
        {
            SaleOrderId = Guid.NewGuid().ToString();
        }
        public SaleOrder(string orderNumber, DateTime orderDate, string customerId)
        {
            SaleOrderId = Guid.NewGuid().ToString();
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            CustomerId = customerId;
        }

        public string SaleOrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
    }

}
