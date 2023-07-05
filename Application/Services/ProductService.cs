using Application.Interfaces;
using Application.ViewModels.Product;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
                return false;
            }
        }

        public async Task<ProductViewModel> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetAsync(x => x.Id == id, "Supplier");

            if (product == null)
            {
                return null;
            }

            return BindModel(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            List<ProductViewModel> models = new List<ProductViewModel>();

            var include = new List<Expression<Func<Product, object>>>()
            {
                s => s.Supplier
            };

            var products = await _productRepository.GetListAsync(include);

            if (products == null)
                return Enumerable.Empty<ProductViewModel>();

            foreach (var item in products)
            {
                models.Add(BindModel(item));
            }

            return models;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsBySupplierIdAsync(string supplierId)
        {
            List<ProductViewModel> models = new List<ProductViewModel>();

            var products = await _productRepository.GetListAsync(x => x.SupplierId == supplierId, "Supplier");

            if (!products.Any())
                return Enumerable.Empty<ProductViewModel>();

            foreach (var item in products)
            {
                models.Add(BindModel(item));
            }

            return models;
        }

        private ProductViewModel BindModel(Product item)
        {
            return
                new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    SupplierId = item.SupplierId,
                    SupplierName = item.Supplier.Name
                };
        }
    }
}
