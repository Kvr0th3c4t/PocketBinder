namespace PocketBinder.Services.CacheService
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory);
        void Remove(string key);
    }

}
