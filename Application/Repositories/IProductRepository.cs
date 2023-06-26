using Application.Interfaces;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryCodeAsync(string categoryCode);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(string categoryId);
    }
}
