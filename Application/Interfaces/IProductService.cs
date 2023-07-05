using Application.ViewModels.Product;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();

        Task<ProductViewModel> GetProductByIdAsync(string id);

        Task<IEnumerable<ProductViewModel>> GetProductsBySupplierIdAsync(string supplierId);

        Task<bool> AddProductAsync(Product product);

    }
}
