using Application.Infrastructure;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Stocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Repositories
{
    public class StockRepositoryUnitTests
    {
        private readonly IStockRepository stockRepository;
        private readonly SouthWestTradersDBContext dbContext;

        public StockRepositoryUnitTests()
        {
            dbContext = MockDbContext.GetContext();
            dbContext.Products.Add(new Product { Name = "Coke", Description = "Coke description", Price = 12.00m });
            dbContext.Products.Add(new Product { Name = "Plane", Description = "Fake plane", Price = 100.00m });
            dbContext.SaveChanges();

            stockRepository = new StockRepository(dbContext);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(2, 15)]
        public async Task AddAsync_ShouldAddStocks(int productId, int availableStock)
        {
            // Arrange
            var stockToBeAdded = new Stock { ProductId = productId, AvailableStock = availableStock };

            // Act
            var stock = await stockRepository.AddAsync(stockToBeAdded);

            // Assert
            Assert.NotNull(stock);
            Assert.Equal(productId, stock.ProductId);
            Assert.Equal(availableStock, stock.AvailableStock);
        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData(40, 30)]
        public async Task UpdateAsync_ShouldUpdatestocks(int oldAvailableStock, int updatedAvailableStock)
        {
            // Arrange
            int stockId = 1;
            int productId = 1;
            var stockToBeUpdated = new Stock { ProductId = productId, AvailableStock = oldAvailableStock };
            await dbContext.Stocks.AddAsync(stockToBeUpdated);
            await dbContext.SaveChangesAsync();

            var stock = dbContext.Stocks.Where(stock => stock.StockId == stockId).FirstOrDefault();
            stock.AvailableStock = updatedAvailableStock;

            // Act
            var updatedstock = await stockRepository.UpdateAsync(stock);

            // Assert
            Assert.NotNull(updatedstock);
            Assert.Equal(stockId, updatedstock.StockId);
            Assert.Equal(productId, updatedstock.ProductId);
            Assert.Equal(updatedAvailableStock, updatedstock.AvailableStock);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(2, 15)]
        public async Task DeleteAsync_ShouldDeleteStocks(int productId, int availableStock)
        {
            // Arrange
            var stockToBeDeleted = new Stock { ProductId = productId, AvailableStock = availableStock };
            await dbContext.Stocks.AddAsync(stockToBeDeleted);
            await dbContext.SaveChangesAsync();

            // Act
            await stockRepository.DeleteAsync(stockToBeDeleted);

            var stock = dbContext.Stocks.Where(stock => stock.StockId == stockToBeDeleted.StockId).FirstOrDefault();

            // Assert
            Assert.Null(stock);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(2, 15)]
        public async Task GetAsync_ShouldRetrieveAstock(int productId, int availableStock)
        {
            // Arrange
            var stockToBeRetrieved = new Stock { ProductId = productId, AvailableStock = availableStock };
            await dbContext.Stocks.AddAsync(stockToBeRetrieved);
            await dbContext.SaveChangesAsync();

            // Act
            var stock = await stockRepository.GetAsync(stock => stock.StockId == stockToBeRetrieved.StockId);

            // Assert
            Assert.NotNull(stock);
            Assert.Equal(productId, stock.ProductId);
            Assert.Equal(availableStock, stock.AvailableStock);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetAsync_ShouldRetrieveNullstock(int stockId)
        {
            // Act
            var stock = await stockRepository.GetAsync(stock => stock.StockId == stockId);

            // Assert
            Assert.Null(stock);
        }

        [Fact]
        public async Task ListAsync_ShouldRetrievestockList()
        {
            // Act
            var stockList = await stockRepository.ListAsync(stock => true);

            // Assert
            Assert.NotNull(stockList);
            Assert.Equal(0, stockList.Count);
        }
    }
}
