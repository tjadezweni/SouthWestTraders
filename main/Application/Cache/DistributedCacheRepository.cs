using Microsoft.Extensions.Caching.Distributed;

namespace Application.Cache;

public class DistributedCacheRepository : IDistributedCacheRepository
{
    private readonly IDistributedCache _distributedCache;


    public DistributedCacheRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions)
    {
        await _distributedCache.SetStringAsync(cacheKey, cacheValue, cacheEntryOptions);
    }

    public async Task<string> GetAsync(string cacheKey)
    {
        string cachedValue = await _distributedCache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(cachedValue) ? string.Empty : cachedValue;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
    }
}