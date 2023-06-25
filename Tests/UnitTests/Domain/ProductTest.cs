using Domain.Entities;
using Shouldly;

namespace UnitTests.Domain
{
    public class ProductTest
    {

        public ProductTest()
        {
                
        }


        [Fact]
        public void Should_Return_Product_Object()
        {
            //Arrange
            var product = new Product("p001", "IPhone 14", 1500, "mobile_phone");

            //Act
            var result = product;

            //Assert
            Assert.NotNull(result);
            result.ShouldNotBeNull();
            result.ProductId.ShouldNotBeNull();
            Assert.Equal("p001",result.Code);
            Assert.Equal("IPhone 14",result.Description);
            Assert.Equal(1500,result.Price);
            Assert.Equal("mobile_phone",result.CategoryId);
        }
    }
}
