using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models;

namespace MP.MKKing.Infra.Context
{
    public class MKKingContext : DbContext
    {
        public MKKingContext(DbContextOptions<MKKingContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}