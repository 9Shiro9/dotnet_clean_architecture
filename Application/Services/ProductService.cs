using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            _unitOfWork.SaveChanges();
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryByIdAsync(string categoryId)
        {
            var deleteCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (deleteCategory != null)
            {
                _categoryRepository.Remove(deleteCategory);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteProductByIdAsync(string productId)
        {
            var deleteProduct = await _productRepository.GetByIdAsync(productId);
            if (deleteProduct != null)
            {
                _productRepository.Remove(deleteProduct);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryCodeAsync(string categoryCode)
        {
            return await _productRepository.GetProductsByCategoryCodeAsync(categoryCode);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(string categoryId)
        {
            return await _productRepository.GetListAsync(x => x.CategoryId == categoryId);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
