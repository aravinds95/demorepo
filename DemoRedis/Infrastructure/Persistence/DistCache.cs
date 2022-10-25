using System.Text;
using System.Text.Json;
using DemoRedis.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace DemoRedis.Infrastructure.Persistence
{
    public class DistCache : IDistCache
    {
        private readonly IDistributedCache _distributedCache;
        public DistCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public void RunMethod()
        {
            Console.WriteLine("test inter");
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class
        {
            var ss = await _distributedCache.GetAsync(key).ConfigureAwait(false);
            var sss = Encoding.UTF8.GetString(ss);
            if (!string.IsNullOrWhiteSpace(sss))
            {
                return JsonSerializer.Deserialize<T>(sss)!;
            }

            return null!;
        }

        public async void SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class
        {
            var str = JsonSerializer.Serialize(value);
            var sss = Encoding.UTF8.GetBytes(str);
            await _distributedCache.SetAsync(key, sss).ConfigureAwait(false);
        }


        public async Task RefreshAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RefreshAsync(key).ConfigureAwait(false);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key).ConfigureAwait(false);
        }
    }
}