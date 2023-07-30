namespace Domain.Entities
{
    public class Customer : BaseAuditEntity
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

        public Customer(string name, string emailAddress, string phoneNumber, string address)
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
