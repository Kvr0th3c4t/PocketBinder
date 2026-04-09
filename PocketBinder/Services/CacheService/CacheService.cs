
using Microsoft.Extensions.Caching.Memory;

namespace PocketBinder.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        // Método genérico para obtener un valor del caché o crearlo si no existe
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        {
            return await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                return await factory();
            });
        }
        // Método para eliminar un valor del caché
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
