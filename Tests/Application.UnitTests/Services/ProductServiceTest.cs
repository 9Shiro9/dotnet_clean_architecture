using Application.Interfaces;
using Application.Services;
using Application.UnitTests.Common;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Application.UnitTests.Services
{
    public class ProductServiceTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ILogger<UnitOfWork>> _unitOfWorkLogger;
        private readonly Mock<ILogger<ProductService>> _productServiceLoger;
        private readonly IProductService _productService;

        public ProductServiceTest()
        {
            //_dbContext = ApplicationContextFactory.Create();
            //_unitOfWorkLogger = new Mock<ILogger<UnitOfWork>>();
            //_productServiceLoger = new Mock<ILogger<ProductService>>();

            //_productService = new ProductService(new UnitOfWork(_dbContext, _unitOfWorkLogger.Object),
            //    new ProductRepository(_dbContext),
            //    new CategoryRepository(_dbContext),
            //    _productServiceLoger.Object);
        }


    }
}
