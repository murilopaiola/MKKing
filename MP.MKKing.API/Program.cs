using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MP.MKKing.Core.Models.Identity;
using MP.MKKing.Infra.Data.Context;
using MP.MKKing.Infra.Data.Context.Identity;
using MP.MKKing.Infra.Data.Seed;

namespace MP.MKKing.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try 
                {
                    var context = services.GetRequiredService<MKKingContext>();
                    await context.Database.MigrateAsync();
                    await MKKingContextSeed.SeedAsync(context, loggerFactory);

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
