using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models;

namespace MP.MKKing.Infra.Data.Context
{
    public class MKKingContext : DbContext
    {
        public MKKingContext(DbContextOptions<MKKingContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // If using Sqlite (thus, in Development)
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // We have to interate through each Entity property and convert from Decimal to Double, so we can sort by Price.
                // This is a necessary workaround, since Sqlite doesn't support decimal types
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}