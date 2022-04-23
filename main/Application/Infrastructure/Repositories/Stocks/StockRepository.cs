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
    }
}
