using Application.Exceptions;
using Application.Features.SearchProductsByName;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.SearchProductsByName
{
    public class SearchProductByNameUnitTests
    {
        [Theory]
        [InlineData("Coke")]
        [InlineData("Cok")]
        [InlineData("Co")]
        [InlineData("coke")]
        [InlineData("cok")]
        [InlineData("co")]
        public async Task SearchProductByName_ShouldRetrieveMatchedProduct(string name)
        {
            // Arrange
            var productName = "Coke";
            var productTest = new Product { ProductId = 1, Name = productName, Description = "desc", Price = 20.00m };
            var mockProductRepository = new Mock<IProductRepository>();
            var cancellationToken = new CancellationToken();
            var query = new SearchProductByName.Query { Name = name };
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(productTest);

            var handler = new SearchProductByName.Handler(mockProductRepository.Object);

            // Act
            var product = await handler.Handle(query, cancellationToken);

            // Assert
            mockProductRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
            Assert.NotNull(product);
            Assert.Same(productName, product.Name);
        }

        [Fact]
        public async Task SearchProductByName_ShouldThrowNotFoundException()
        {
            // Arrange
            var productName = "Coke";
            Product? productTest = null;
            var mockProductRepository = new Mock<IProductRepository>();
            var cancellationToken = new CancellationToken();
            var query = new SearchProductByName.Query { Name = "Copenhagen" };
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(productTest);

            var handler = new SearchProductByName.Handler(mockProductRepository.Object);

            // Act
            var action = () => handler.Handle(query, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(action);
            mockProductRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
        }
    }
}
