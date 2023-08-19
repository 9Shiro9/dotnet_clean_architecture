using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<string> AddProductAsync(CreateProductDto createProduct)
        {
            try
            {
                var product = new Product(createProduct.Code,createProduct.Description,createProduct.CategoryId,createProduct.BuyingPrice,createProduct.SellingPrice,createProduct.Quantity);

                await _productRepository.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return product.ProductId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ExceptionType}: {Message}", ex.GetType().Name, ex.Message);
                return string.Empty;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return BindData(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var productDTOs = new List<ProductDto>();

            var products = await _productRepository.GetListAsync();

            foreach (var product in products)
            {
                productDTOs.Add(BindData(product));
            }

            return productDTOs;
        }
        private ProductDto BindData(Product item)
        {
            return
                new ProductDto()
                {
                    Id = item.ProductId,
                    Code = item.Code,
                    Description = item.Description,
                    SellingPrice = item.SellingPrice,
                    BuyingPrice = item.BuyingPrice,
                    Quantity = item.Quantity
                };
        }

       
    }
}
