using Application.Exceptions;
using Application.Features.DecreaseStocks;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.DecreaseStocks
{
    public class DecreaseStockUnitTests
    {
        [Fact]
        public async Task DecreaseStock_ShouldIncreaseStockAmount()
        {
            // Arrange
            int mockProductId = 1;
            int mockAvailableStock = 0;
            int stockIncreaseAmount = 10;
            var mockStock = new Stock { ProductId = mockProductId, AvailableStock = 20 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStockRepository = new Mock<IStockRepository>();
            mockStockRepository.Setup(mock => mock.GetStockWithProductId(It.Is<int>(productId => productId == mockProductId)))
                .ReturnsAsync(mockStock);

            var command = new DecreaseStock.Command { ProductId = mockProductId, StockAmount = stockIncreaseAmount };
            var cancellationToken = new CancellationToken();

            var handler = new DecreaseStock.Handler(mockStockRepository.Object, mockUnitOfWork.Object);

            // Act
            await handler.Handle(command, cancellationToken);

            // Assert
            mockStockRepository.Verify(mock => mock.GetStockWithProductId(It.Is<int>(productId => productId == mockProductId)), Times.Once());
            mockUnitOfWork.Verify(mock => mock.CompleteAsync(), Times.Once());
        }

        [Fact]
        public async Task DecreaseStock_ShouldThrowException()
        {
            // Arrange
            int mockProductId = 1;
            int mockAvailableStock = 0;
            int stockIncreaseAmount = 10;
            var mockStock = new Stock { ProductId = mockProductId, AvailableStock = 0 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStockRepository = new Mock<IStockRepository>();
            mockStockRepository.Setup(mock => mock.GetStockWithProductId(It.Is<int>(productId => productId == mockProductId)))
                .Returns(Task.FromResult<Stock?>(null));

            var command = new DecreaseStock.Command { ProductId = mockProductId, StockAmount = stockIncreaseAmount };
            var cancellationToken = new CancellationToken();

            var handler = new DecreaseStock.Handler(mockStockRepository.Object, mockUnitOfWork.Object);

            // Act
            var action = () => handler.Handle(command, cancellationToken);

            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(action);
            mockStockRepository.Verify(mock => mock.GetStockWithProductId(It.Is<int>(productId => productId == mockProductId)), Times.Once());
            mockUnitOfWork.Verify(mock => mock.CompleteAsync(), Times.Never());
        }
    }
}
