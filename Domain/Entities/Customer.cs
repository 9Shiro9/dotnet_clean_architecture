namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        
        //Identity UserId
        public string UserId { get; set; }
    }
}
