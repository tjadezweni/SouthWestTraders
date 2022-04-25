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
using Application.Infrastructure.SeedWork;

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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new Product());
            var cancellationToken = new CancellationToken();
            var handler = new RemoveProduct.Handler(mockProductRepository.Object, mockUnitOfWork.Object);

            // Act
            var unitValue = await handler.Handle(command, cancellationToken);

            // Assert
            mockUnitOfWork.Verify(mock => mock.CompleteAsync(), Times.Once());
            Assert.Equal(Unit.Value, unitValue);
            mockProductRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task RemoveProduct_ShouldThrowException()
        {
            // Arrange
            int productId = 1;
            var command = new RemoveProduct.Command { ProductId = productId };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(null as Product);
            var cancellationToken = new CancellationToken();
            var handler = new RemoveProduct.Handler(mockProductRepository.Object, mockUnitOfWork.Object);

            // Act
            var action = () => handler.Handle(command, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(action);
        }
    }
}
