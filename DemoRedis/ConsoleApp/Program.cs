using System;
using DemoRedis.Application.Interfaces;
using DemoRedis.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Memory;

namespace DemoRedis.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddDistributedMemoryCache();
        services.AddScoped<IDistCache, DistCache>();
    })
    .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var distCache = services.GetRequiredService<IDistCache>();
                distCache.SetAsync<TestClass>("testclass", new TestClass
                {
                    MyProperty = 100
                });

                var tt = await distCache.GetAsync<TestClass>("testclass").ConfigureAwait(false);

                Console.WriteLine(tt.MyProperty);
            }

            await host.StopAsync();
            //  Console.WriteLine("test");
        }
    }
}