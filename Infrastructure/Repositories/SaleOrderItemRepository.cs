using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class SaleOrderItemRepository : EfBaseRepository<SaleOrderItem>, ISaleOrderItemRepository
    {
        public SaleOrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
