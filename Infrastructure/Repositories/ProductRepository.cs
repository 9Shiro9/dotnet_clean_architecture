using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : EfBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(string supplierId)
        {
            return await _dbContext.Products.Include(x => x.Supplier).Where(x => x.SupplierId == supplierId).ToListAsync();
        }
    }
}
