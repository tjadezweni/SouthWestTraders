using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.OrderStates
{
    public class OrderStateRepository : AsyncRepository<OrderState>, IOrderStateRepository
    {
        public OrderStateRepository(SouthWestTradersDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
