using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories.Orders
{
    public class OrderRepository : AsyncRepository<Order>, IOrderRepository
    {
        public OrderRepository(SouthWestTradersDBContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<Order?> GetOrderByIdEager(int orderId)
        {
            return await _dbSet.Include(order => order.OrderState)
                .Where(order => order.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public Task<Order?> GetOrderWithOrderState(int orderId)
        {
            return _dbSet.Include(order => order.OrderState)
                .Where(order => order.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public Task<Order?> SearchOrderByName(string name)
        {
            return _dbSet.Include(order => order.OrderState)
                .Where(order => order.Name.ToLower().Contains(name))
                .FirstOrDefaultAsync();
        }

        public Task<List<Order>> SearchOrdersByDate(int day, int month, int year)
        {
            return _dbSet.Include(order => order.OrderState)
                .Where(order => order.CreatedDateUtc.Day == day && 
                order.CreatedDateUtc.Month == month && order.CreatedDateUtc.Year == year)
                .ToListAsync();
        }
    }
}
