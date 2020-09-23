using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Infra.Data.Context;
using MP.MKKing.Infra.Data.Repositories;
using StackExchange.Redis;

namespace MP.MKKing.Infra.CrossCutting.Bootstrapper
{
    /// <summary>
    /// Register services to a container
    /// </summary>
    public static class RegisterServices
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            //DbContext
            services.AddDbContext<MKKingContext>(d => 
                d.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.RegisterRepositories();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}