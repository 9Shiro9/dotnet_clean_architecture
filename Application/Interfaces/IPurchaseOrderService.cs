using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync();

        Task<PurchaseOrder> GetPurchaseOrderByIdAsync(string purchaseOrderId);

        Task<bool> AddPurchaseOrder(PurchaseOrder purchaseOrder,IEnumerable<PurchaseOrderItem> purchaseOrderItems);
    }
}
