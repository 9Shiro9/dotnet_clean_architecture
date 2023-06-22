using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(string productId)
        {
            var _deleteProduct = await _productRepository.GetByIdAsync(productId);

            if (_deleteProduct != null)
            {
                _productRepository.Remove(_deleteProduct);
            }

            await _productRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(string categoryId)
        {
            return await _productRepository.GetAsync(x => x.CategoryId == categoryId);
        }

        public void UpdateProductByIdAsync(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
