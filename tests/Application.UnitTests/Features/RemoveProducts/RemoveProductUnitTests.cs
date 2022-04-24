using Application.Features.RemoveProducts;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Threading;
using MediatR;
using Application.Exceptions;

namespace Application.UnitTests.Features.RemoveProducts
{
    public class RemoveProductUnitTests
    {
        [Fact]
        public async Task RemoveProduct_ShouldRemoveProduct()
        {
            // Arrange
            int productId = 1;
            var command = new RemoveProduct.Command { ProductId = productId };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new Product());
            var cancellationToken = new CancellationToken();
            var handler = new RemoveProduct.Handler(mockProductRepository.Object);

            // Act
            var unitValue = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.Equal(Unit.Value, unitValue);
            mockProductRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task RemoveProduct_ShouldThrowException()
        {
            // Arrange
            int productId = 1;
            var command = new RemoveProduct.Command { ProductId = productId };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(null as Product);
            var cancellationToken = new CancellationToken();
            var handler = new RemoveProduct.Handler(mockProductRepository.Object);

            // Act
            var action = () => handler.Handle(command, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(action);
        }
    }
}
