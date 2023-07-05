using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class SupplierRepository : EfBaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Supplier>> GetSuppliersByNameAsync(string supplierName)
        {
            throw new NotImplementedException();
        }
    }
}
