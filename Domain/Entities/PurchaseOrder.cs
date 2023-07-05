namespace Domain.Entities
{
    public class PurchaseOrder : BaseAuditEntity
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        // Additional properties as needed

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
