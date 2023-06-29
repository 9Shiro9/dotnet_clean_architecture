using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> AddProductsAsync(IEnumerable<Product> products)
        {
            try
            {
                await _productRepository.AddRangeAsync(products);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
                return false;
            }
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(string supplierId)
        {
            return await _productRepository.GetProductsBySupplierIdAsync(supplierId);
        }
    }
}
