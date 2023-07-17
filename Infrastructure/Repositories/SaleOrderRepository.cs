using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SaleOrderRepository : EfBaseRepository<SaleOrder>, ISaleOrderRepository
    {
        public SaleOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<SaleOrder>> GetSaleOrdersByCustomerIdAsync(string customerId)
        {
            return await _dbContext.SaleOrders
                 .Include(x => x.SaleOrderItems)
                 .Include(x => x.Customer)
                 .Where(x => x.CustomerId == customerId)
                 .ToListAsync();
        }
    }
}
