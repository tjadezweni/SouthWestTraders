using Application.Features.ViewProductsList;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.ViewProductsList
{
    public class ViewProductsUnitTests
    {
        [Fact]
        public async Task ViewProducts_ShouldRetrieveEmptyList()
        {
            // Arrange
            var emptyProductList = new List<Product>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(emptyProductList);

            var query = new ViewProducts.Query();
            var cancellationToken = new CancellationToken();
            var handler = new ViewProducts.Handler(mockProductRepository.Object);

            // Act
            var productList = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(productList);
            Assert.Empty(productList);
        }

        [Fact]
        public async Task ViewProducts_ShouldRetrievePopulatedList()
        {
            // Arrange
            var populatedProductList = new List<Product>()
            {
                new Product{Name = "Test", Description = "Test", Price = 20},
                new Product{Name = "Test", Description = "Test", Price = 30}
            };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(populatedProductList);

            var query = new ViewProducts.Query();
            var cancellationToken = new CancellationToken();
            var handler = new ViewProducts.Handler(mockProductRepository.Object);

            // Act
            var productList = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(productList);
            Assert.NotEmpty(productList);
        }
    }
}
