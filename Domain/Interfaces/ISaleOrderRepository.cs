namespace Domain.Interfaces
{
    public interface ISaleOrderRepository : IBaseRepository<SaleOrder>
    {
        Task<IReadOnlyList<SaleOrder>> GetSaleOrdersByCustomerIdAsync(string customerId);
    }
}
