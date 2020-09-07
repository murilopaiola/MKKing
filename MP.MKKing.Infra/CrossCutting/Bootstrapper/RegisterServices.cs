using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Infra.Data.Context;
using MP.MKKing.Infra.Data.Repositories;

namespace MP.MKKing.Infra.CrossCutting.Bootstrapper
{
    /// <summary>
    /// Register services to a container
    /// </summary>
    public static class RegisterServices
    {
        public static void Register(this IServiceCollection services, string connectionString)
        {
            //DbContext
            services.AddDbContext<MKKingContext>(d => 
                d.UseSqlite(connectionString));

            services.RegisterRepositories();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}