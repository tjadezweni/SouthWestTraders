using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Orders
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order?> GetOrderWithOrderState(int orderId);
        Task<Order?> SearchOrderByName(string name);
        Task<Order?> GetOrderByIdEager(int orderId);
        Task<List<Order>> SearchOrdersByDate(int day, int month, int year);
    }
}
