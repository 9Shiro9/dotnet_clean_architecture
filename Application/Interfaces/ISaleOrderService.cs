using Application.DTOs.SaleOrder;

namespace Application.Interfaces
{
    public interface ISaleOrderService
    {
        Task<IEnumerable<SaleOrderDto>> GetSaleOrdersAsync();
        Task<IEnumerable<SaleOrderDto>> GetSaleOrdersByCustomerIdAsync(string customerId);
        Task<SaleOrderDto> GetSaleOrderByIdAsync(string orderId);
        Task<string> CreateSaleOrder(CreateSaleOrderDto createOrder);

    }
}
