namespace Domain.Entities
{
    public class CustomerMember
    {
        public string Id { get; set; }  
        public string MemberCode { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public bool IsExchangePoint { get; set; }

    }
}
