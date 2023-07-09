using Application.DTOs.PurchaseOrder;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync();

        Task<PurchaseOrderDto> GetPurchaseOrderByIdAsync(string purchaseOrderId);

        Task<string> CreatePurchaseOrder(CreatePurchaseOrderDto createOrder);
    }
}
