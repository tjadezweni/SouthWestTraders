using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Stocks
{
    public interface IStockRepository : IAsyncRepository<Stock>
    {
        public Task<Stock?> GetStockWithProductId(int productId);
    }
}
