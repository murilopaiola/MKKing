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
        }
    }
}