using Application.Services;
using Application.UnitTests.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Application.UnitTests.Services
{
    public class ProductServiceTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ILogger<UnitOfWork>> _unitOfWorkLogger;
        private readonly Mock<ILogger<ProductService>> _productServiceLoger;
        private readonly IProductService _productService;

        public ProductServiceTest()
        {
            _dbContext = ApplicationContextFactory.Create();
            _unitOfWorkLogger = new Mock<ILogger<UnitOfWork>>();
            _productServiceLoger = new Mock<ILogger<ProductService>>();

            _productService = new ProductService(new UnitOfWork(_dbContext, _unitOfWorkLogger.Object),
                new ProductRepository(_dbContext),
                new CategoryRepository(_dbContext),
                _productServiceLoger.Object);
        }

        [Fact]
        public async void AddCategoryAsync_and_GetCategoriesAsync_Should_Return_Categories()
        {
            //Arrange
            var category = new Category("c001_code", "c001_desp");


            //Act
            await _productService.AddCategoryAsync(category);

            var categories = await _productService.GetCategoriesAsync();

            //Assert
            categories.ShouldNotBe(null);
            categories.Count().ShouldBe(1);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
