using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MP.MKKing.Infra.Context;

namespace MP.MKKing.Infra.Bootstrapper
{
    public static class RegisterServices
    {
        public static void Register(this IServiceCollection services, string connectionString)
        {
            //DbContext
            services.AddDbContext<MKKingContext>(d => 
                d.UseSqlite(connectionString));
        }
    }
}