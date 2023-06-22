using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(string categoryId);

        Task AddProductAsync(Product product);

        void UpdateProductByIdAsync(Product product);

        Task DeleteProductByIdAsync(string productId);
    }
}
