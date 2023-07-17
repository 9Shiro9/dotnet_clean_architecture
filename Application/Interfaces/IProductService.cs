using Application.DTOs.Product;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductByIdAsync(string id);

        Task<string> AddProductAsync(CreateProductDto createProduct);

    }
}
