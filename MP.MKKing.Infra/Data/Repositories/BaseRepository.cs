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
    /// <summary>
    /// A generic repository of type T
    /// </summary>
    /// <typeparam name="T">Our DTO entity</typeparam>
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

        /// <summary>
        /// Get entity applying specification <see cref="ApplySpecification"/>
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

        /// <summary>
        /// List all entities applying specification <see cref="ApplySpecification"/>
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) => await ApplySpecification(spec).ToListAsync();

        /// <summary>
        /// Count items of a collection under a certain specification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(ISpecification<T> spec) => await ApplySpecification(spec).CountAsync();

        /// <summary>
        /// Apply specification to an <see cref="IQueryable"/>
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQueryable(DbSet.AsQueryable(), spec);

    }
}