using Application.Infrastructure;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.OrderStates;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Repositories
{
    public class OrderStateRepositoryUnitTests
    {
        private readonly IOrderStateRepository orderStateRepository;
        private readonly SouthWestTradersDBContext dbContext;

        public OrderStateRepositoryUnitTests()
        {
            dbContext = MockDbContext.GetContext();
            orderStateRepository = new OrderStateRepository(dbContext);
        }

        [Theory]
        [InlineData("Reserved")]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public async Task AddAsync_ShouldAddOrderStates(string state)
        {
            // Arrange
            var orderStateToBeAdded = new OrderState { State = state };

            // Act
            var orderState = await orderStateRepository.AddAsync(orderStateToBeAdded);

            // Assert
            Assert.NotNull(orderState);
            Assert.Same(state, orderState.State);
        }

        [Theory]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public async Task UpdateAsync_ShouldUpdateOrderStates(string state)
        {
            // Arrange
            int orderStateId = 1;
            var orderStateReserved = new OrderState { State = state };
            await dbContext.OrderStates.AddAsync(orderStateReserved);
            await dbContext.SaveChangesAsync();

            var orderState = dbContext.OrderStates.Where(orderState => orderState.OrderStateId == orderStateId).FirstOrDefault();
            orderState.State = state;

            // Act
            var updatedOrderState = await orderStateRepository.UpdateAsync(orderState);

            // Assert
            Assert.NotNull(orderState);
            Assert.Equal(orderStateId, orderState.OrderStateId);
            Assert.Same(state, orderState.State);
        }

        [Theory]
        [InlineData("Reserved")]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public async Task DeleteAsync_ShouldDeleteOrderStates(string state)
        {
            // Arrange
            var orderStateToBeDeleted = new OrderState { State = state };
            await dbContext.OrderStates.AddAsync(orderStateToBeDeleted);
            await dbContext.SaveChangesAsync();

            // Act
            await orderStateRepository.DeleteAsync(orderStateToBeDeleted);

            var orderState = dbContext.OrderStates.Where(orderState => orderState.OrderStateId == orderStateToBeDeleted.OrderStateId).FirstOrDefault();

            // Assert
            Assert.Null(orderState);
        }

        [Theory]
        [InlineData("Reserved")]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public async Task GetAsync_ShouldRetrieveAOrderState(string state)
        {
            // Arrange
            var orderStateToBeRetrieved = new OrderState { State = state };
            await dbContext.OrderStates.AddAsync(orderStateToBeRetrieved);
            await dbContext.SaveChangesAsync();

            // Act
            var orderState = await orderStateRepository.GetAsync(orderState => orderState.OrderStateId == orderStateToBeRetrieved.OrderStateId);

            // Assert
            Assert.NotNull(orderState);
            Assert.Same(state, orderState.State);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetAsync_ShouldRetrieveNullorderState(int orderStateId)
        {
            // Act
            var orderState = await orderStateRepository.GetAsync(orderState => orderState.OrderStateId == orderStateId);

            // Assert
            Assert.Null(orderState);
        }

        [Fact]
        public async Task ListAsync_ShouldRetrieveorderStateList()
        {
            // Act
            var orderStateList = await orderStateRepository.ListAsync(orderState => true);

            // Assert
            Assert.NotNull(orderStateList);
            Assert.Empty(orderStateList);
        }
    }
}
