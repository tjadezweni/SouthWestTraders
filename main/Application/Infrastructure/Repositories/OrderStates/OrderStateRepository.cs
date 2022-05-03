using Application.Cache;
using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Application.Infrastructure.Repositories.OrderStates
{
    public class OrderStateRepository : AsyncRepository<OrderState>, IOrderStateRepository
    {
        private readonly IDistributedCacheRepository _distributedCacheRepository;
        private readonly string _cacheKey;
        private readonly int _absoluteExpiration;

        public OrderStateRepository(SouthWestTradersDBContext dbContext, IDistributedCacheRepository distributedCacheRepository)
            : base(dbContext)
        {
            _absoluteExpiration = 5;
            _cacheKey = "OrderStates";
            _distributedCacheRepository = distributedCacheRepository;
        }

        public async Task<IEnumerable<OrderState>> GetCacheAsync()
        {
            var cachedOrderStates = await _distributedCacheRepository.GetAsync(_cacheKey);
            if (!string.IsNullOrEmpty(cachedOrderStates))
            {
                var orderStates = JsonConvert.DeserializeObject<IEnumerable<OrderState>>(cachedOrderStates);
                return orderStates;
            }

            var orderStatesList = await ListAsync(orderState => true);
            var jsonOrderStatesList = JsonConvert.SerializeObject(orderStatesList);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(_absoluteExpiration));
            await _distributedCacheRepository.SetAsync(_cacheKey, jsonOrderStatesList, options);
            return orderStatesList;
        }

        public async Task<OrderState> GetOrderStateById(int orderStateId)
        {
            var orderStatesList = await GetCacheAsync();
            return orderStatesList.Where(orderState => orderState.OrderStateId == orderStateId).FirstOrDefault();
        }

    }
}
