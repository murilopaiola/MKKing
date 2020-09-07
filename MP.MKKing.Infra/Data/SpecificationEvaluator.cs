using System.Linq;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;

namespace MP.MKKing.Infra.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseModel
    {
        public static IQueryable<TEntity> GetQueryable(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (null != spec.Criteria)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}