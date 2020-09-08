using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// Specification interface implemented by the generic specification class <see cref="BaseSpecification{T}"/>
    /// Enables Filtering, Including, Ordering and Pagination
    /// </summary>
    /// <typeparam name="T">The DTO</typeparam>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}