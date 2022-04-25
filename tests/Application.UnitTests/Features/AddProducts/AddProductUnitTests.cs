using Application.Features.AddProducts;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using Moq;
using System;
using System.Linq.Expressions;
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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(mock => mock.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(Task.FromResult<Product?>(null));
            var mockStockRepository = new Mock<IStockRepository>();
            var handler = new AddProduct.Handler(mockUnitOfWork.Object, mockProductRepository.Object, mockStockRepository.Object);

            // Act
            var product = await handler.Handle(command, cancellationToken);

            // Assert
            mockProductRepository.Verify(mock => mock.AddAsync(It.IsAny<Product>()), Times.Once());
            mockStockRepository.Verify(mock => mock.AddAsync(It.IsAny<Stock>()), Times.Once());
            mockUnitOfWork.Verify(mock => mock.CompleteAsync(), Times.Once());
            Assert.NotNull(product);
            Assert.Same(name, product.Name);
            Assert.Same(description, product.Description);
            Assert.Equal(price, product.Price);
        }
    }
}
