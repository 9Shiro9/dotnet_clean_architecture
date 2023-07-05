using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class PurchaseOrderRepository : EfBaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
