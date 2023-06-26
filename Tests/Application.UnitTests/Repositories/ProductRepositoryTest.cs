using Application.Repositories;
using Application.UnitTests.Common;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Shouldly;

namespace Application.UnitTests.Repositories
{
    public class ProductRepositoryTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        public ProductRepositoryTest()
        {
            //Arrange
            _dbContext = ApplicationContextFactory.Create();

            SeedData(_dbContext);

            _productRepository = new ProductRepository(_dbContext);
        }



        [Fact]
        public async void GetListAsync_Should_Return_Product_List()
        {
            var products = await _productRepository.GetListAsync();

            //Assert
            products.Count.ShouldBe(2);
            products.ShouldNotBe(null);
        }

        [Fact]
        public async void GetProductsByCategoryCodeAsync_ShouldReturn_List()
        {
            //Act
            var products = await _productRepository.GetProductsByCategoryCodeAsync("category_code_1");

            //Assert
            products.Count().ShouldBe(1);
            products.ShouldNotBe(null);
            products.First().CategoryId.ShouldBe("c001");
            products.First().ProductId.ShouldBe("p001");
        }

        [Fact]
        public async void GetProductsByCategoryIdAsync_ShouldReturn_Product_List()
        {
            //Act
            var products = await _productRepository.GetProductsByCategoryIdAsync("c001");

            //Assert
            products.ShouldNotBe(null);
            products.Count().ShouldBe(1);
            products.First().ProductId.ShouldBe("p001");
        }

        private void SeedData(ApplicationDbContext context)
        {
            context.Categories.AddRange(new[] {
                new Category("category_code_1","category_desp_1"){ CategoryId = "c001" },
                new Category("category_code_2","category_desp_2"){ CategoryId = "c002" },
            });

            context.Products.AddRange(new[] {
                new Product("p001_code","p001_desp",100,"c001"){ ProductId = "p001" },
                 new Product("p002_code","p002_desp",100,"c002"){ ProductId = "p002" },
            });
            context.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
