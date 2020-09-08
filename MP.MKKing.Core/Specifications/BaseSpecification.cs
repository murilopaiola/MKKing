using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// A generic specification class for DTOs of type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">The DTO</typeparam>
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria) => Criteria = criteria;

        public BaseSpecification() { }

        public Expression<Func<T, bool>> Criteria { get; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;
        
    }
}