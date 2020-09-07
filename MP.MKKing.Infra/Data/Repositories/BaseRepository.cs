using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;
using MP.MKKing.Infra.Data.Context;

namespace MP.MKKing.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected MKKingContext _context;
        protected DbSet<T> DbSet;

        public BaseRepository(MKKingContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await DbSet.FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAllAsync() => await DbSet.ToListAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> specification) => await ApplySpecification(specification).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification) => await ApplySpecification(specification).ToListAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> specification) => SpecificationEvaluator<T>.GetQueryable(DbSet.AsQueryable(), specification);
    }
}