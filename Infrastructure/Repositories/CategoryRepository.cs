using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : EfBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
