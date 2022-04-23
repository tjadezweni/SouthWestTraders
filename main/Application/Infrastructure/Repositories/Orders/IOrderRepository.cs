using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Orders
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}
