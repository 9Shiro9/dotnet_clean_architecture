using Application.Repositories;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : EfBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryCodeAsync(string categoryCode)
        {
            return await _dbContext.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Code == categoryCode)
                .ToListAsync();

        }
    }
}
