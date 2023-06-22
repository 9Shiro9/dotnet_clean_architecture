using Domain.Common;

namespace Domain.Entities
{
    public class Order : BaseAuditEntity
    {
        public string OrderId { get; set; }
        public DateTime OrderDated { get; set; }

        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public int TotalQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<Order> OrderItems { get; set; }

        public Order(string _description)
        {
            OrderId = Guid.NewGuid().ToString();
            OrderItems = new List<Order>();
            OrderDated = DateTime.Now;
            Description = _description;
        }

        public void SetTotalQuantity()
        {
            TotalQuantity = OrderItems.Count;
        }

        public void SetTotalPrice()
        {
            TotalPrice = OrderItems.Sum(x => x.TotalPrice);
        }
    }
}
