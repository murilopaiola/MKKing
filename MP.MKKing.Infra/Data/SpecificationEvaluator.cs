using System.Linq;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;

namespace MP.MKKing.Infra.Data
{
    /// <summary>
    /// Evaluator to apply specifications to an IQueryable <see cref="GetQueryable"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseModel
    {
        public static IQueryable<TEntity> GetQueryable(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (null != spec.Criteria)
                query = query.Where(spec.Criteria);

            if (null != spec.OrderBy)
                query = query.OrderBy(spec.OrderBy);

            if (null != spec.OrderByDescending)
                query = query.OrderByDescending(spec.OrderByDescending);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}