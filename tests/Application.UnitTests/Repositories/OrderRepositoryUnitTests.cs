using Application.Infrastructure;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Orders;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Repositories
{
    public class OrderRepositoryUnitTests
    {
        private readonly IOrderRepository orderRepository;
        private readonly SouthWestTradersDBContext dbContext;

        public OrderRepositoryUnitTests()
        {
            dbContext = MockDbContext.GetContext();
            dbContext.Products.Add(new Product { Name = "Coke", Description = "A nice beverage", Price = 12.00m });
            dbContext.OrderStates.Add(new OrderState { State = "Reserved" });
            dbContext.OrderStates.Add(new OrderState { State = "Complete" });
            dbContext.SaveChanges();

            orderRepository = new OrderRepository(dbContext);
        }

        [Theory]
        [InlineData("Coke", 1, 1)]
        [InlineData("Waffles", 1, 2)]
        public async Task AddAsync_ShouldAddOrders(string name, int productId, int orderStateId)
        {
            // Arrange
            var orderToBeAdded = new Order { Name = name, ProductId = productId, OrderStateId = orderStateId };

            // Act
            var order = await orderRepository.AddAsync(orderToBeAdded);

            // Assert
            Assert.NotNull(order);
            Assert.Same(name, order.Name);
            Assert.Equal(productId, order.ProductId);
            Assert.Equal(orderStateId, order.OrderStateId);
        }

        [Theory]
        [InlineData("Coke", 1, 1, 2)]
        public async Task UpdateAsync_ShouldUpdateOrders(string name, int productId, int oldOrderStateId, int updatedOrderStateId)
        {
            // Arrange
            int orderId = 1;
            var orderMacbookAir = new Order { Name = "Macbook Air", ProductId = productId, OrderStateId = oldOrderStateId };
            await dbContext.Orders.AddAsync(orderMacbookAir);
            await dbContext.SaveChangesAsync();

            var order = dbContext.Orders.Where(order => order.OrderId == orderId).FirstOrDefault();
            order.Name = name;
            order.OrderStateId = updatedOrderStateId;

            // Act
            var updatedorder = await orderRepository.UpdateAsync(order);

            // Assert
            Assert.NotNull(updatedorder);
            Assert.Equal(orderId, updatedorder.OrderId);
            Assert.Same(name, updatedorder.Name);
            Assert.Equal(productId, updatedorder.ProductId);
        }

        [Theory]
        [InlineData("Sriracha", 1, 1)]
        [InlineData("BBQ", 1, 2)]
        public async Task DeleteAsync_ShouldDeleteOrders(string name, int productId, int  orderStateId)
        {
            // Arrange
            var orderToBeDeleted = new Order { Name = name, ProductId =productId, OrderStateId = orderStateId };
            await dbContext.Orders.AddAsync(orderToBeDeleted);
            await dbContext.SaveChangesAsync();

            // Act
            await orderRepository.DeleteAsync(orderToBeDeleted);

            var order = dbContext.Orders.Where(order => order.OrderId == orderToBeDeleted.OrderId).FirstOrDefault();

            // Assert
            Assert.Null(order);
        }

        [Theory]
        [InlineData("Sriracha", 1, 1)]
        [InlineData("BBQ", 1, 2)]
        public async Task GetAsync_ShouldRetrieveAOrder(string name, int productId, int orderStateId)
        {
            // Arrange
            var orderToBeRetrieved = new Order { Name = name, ProductId = productId, OrderStateId = orderStateId };
            await dbContext.Orders.AddAsync(orderToBeRetrieved);
            await dbContext.SaveChangesAsync();

            // Act
            var order = await orderRepository.GetAsync(order => order.OrderId == orderToBeRetrieved.OrderId);

            // Assert
            Assert.NotNull(order);
            Assert.Same(name, order.Name);
            Assert.Equal(productId, order.ProductId);
            Assert.Equal(orderStateId, order.OrderStateId);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetAsync_ShouldRetrieveNullOrder(int orderId)
        {
            // Act
            var order = await orderRepository.GetAsync(order => order.OrderId == orderId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public async Task ListAsync_ShouldRetrieveOrderList()
        {
            // Act
            var orderList = await orderRepository.ListAsync(order => true);

            // Assert
            Assert.NotNull(orderList);
            Assert.Empty(orderList);
        }


    }
}
