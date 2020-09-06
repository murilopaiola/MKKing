using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MP.MKKing.Infra.Context
{
    public class MKKingContextFactory : IDesignTimeDbContextFactory<MKKingContext>
    {
        public MKKingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MKKingContext>();
            optionsBuilder.UseSqlite("Data Source=MKKing.db");

            return new MKKingContext(optionsBuilder.Options);
        }
    }
}