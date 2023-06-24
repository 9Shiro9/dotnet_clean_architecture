using Application.Repositories;
using Domain.Entities;
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
