using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.OrderStates
{
    public interface IOrderStateRepository : IAsyncRepository<OrderState>
    {
        Task<IEnumerable<OrderState>> GetCacheAsync();
        Task<OrderState> GetOrderStateById(int orderStateId);
    }
}
