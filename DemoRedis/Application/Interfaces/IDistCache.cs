namespace DemoRedis.Application.Interfaces
{
    public interface IDistCache
    {
        void RunMethod();

        Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

        void SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;

        Task RefreshAsync(string key, CancellationToken cancellationToken = default);

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}