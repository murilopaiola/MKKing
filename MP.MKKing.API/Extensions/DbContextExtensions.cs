using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.MKKing.Infra.Data.Context;
using MP.MKKing.Infra.Data.Context.Identity;
using StackExchange.Redis;

namespace MP.MKKing.API.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<MKKingContext>(d => 
                d.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(o => 
            {
                o.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            return services;
        }
    }
}