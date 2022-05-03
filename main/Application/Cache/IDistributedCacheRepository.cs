using Microsoft.Extensions.Caching.Distributed;

namespace Application.Cache;

public interface IDistributedCacheRepository
{
    Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions);

    Task<string> GetAsync(string cacheKey);

    Task RemoveAsync(string cacheKey);
}