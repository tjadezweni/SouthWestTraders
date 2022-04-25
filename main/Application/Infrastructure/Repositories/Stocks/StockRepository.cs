using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Stocks
{
    public class StockRepository : AsyncRepository<Stock>, IStockRepository
    {
        public StockRepository(SouthWestTradersDBContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<Stock?> GetStockWithProductId(int productId)
        {
            return _dbSet.Where(stock => stock.ProductId == productId).FirstOrDefault();
        }
    }
}
