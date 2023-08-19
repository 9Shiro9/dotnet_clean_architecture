using Domain.Entities;
namespace Domain.UnitTests.Entities
{
    public class ProductTest
    {
        [Fact]
        public void ShouldBeEqualCtorAndPropValues()
        {
            //Arrange
            string _code = "P001";
            string _description = "P001Desp";
            string _categoryId = "F77F2133-F1E8-40EC-8B60-DD8FB2FE0017";
            decimal _buyingPrice = 90;
            decimal _sellingPrice =100;
            int _qty = 10;

            //Act
            var product = new Product(_code, _description, _categoryId, _buyingPrice, _sellingPrice, _qty);

            //Assert
            Assert.NotNull(product.ProductId);
            Assert.Equal(_code, product.Code);
            Assert.Equal(_description, product.Description);
            Assert.Equal(_categoryId, product.CategoryId);
            Assert.Equal(_buyingPrice, product.BuyingPrice);
            Assert.Equal(_sellingPrice, product.SellingPrice);
            Assert.Equal(_qty, product.Quantity);

        }

        [Fact]
        public void ShuldBeNotNullProductId()
        {
            var product = new Product();

            Assert.NotNull(product.ProductId);
        }
    }
}
