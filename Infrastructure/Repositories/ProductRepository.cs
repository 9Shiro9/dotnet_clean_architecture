using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class ProductRepository : EfBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
