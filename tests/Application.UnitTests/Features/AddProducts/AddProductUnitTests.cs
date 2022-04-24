using Application.Features.AddProducts;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.AddProducts
{
    public class AddProductUnitTests
    {
        [Theory]
        [InlineData("Coke 2L", "A fizzy drink", 20.00)]
        [InlineData("Fanta 1.5L", "A elite fizzy drink", 19.99)]
        public async Task AddProduct_ShouldAddProduct(string name, string description, decimal price)
        {
            // Arrange
            var command = new AddProduct.Command { Name = name, Description = description, Price = price };
            var cancellationToken = new CancellationToken();
            var mockProductRepository = new Mock<IProductRepository>();
            var handler = new AddProduct.Handler(mockProductRepository.Object);

            // Act
            var product = await handler.Handle(command, cancellationToken);

            // Assert
            mockProductRepository.Verify(mock => mock.AddAsync(It.IsAny<Product>()), Times.Once());
            Assert.NotNull(product);
            Assert.Same(name, product.Name);
            Assert.Same(description, product.Description);
            Assert.Equal(price, product.Price);
        }
    }
}
