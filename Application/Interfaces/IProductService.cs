using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(string id);

        Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(string supplierId);

        Task<bool> AddProductsAsync(IEnumerable<Product> products);

    }
}
