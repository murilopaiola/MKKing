using System.Collections.Generic;
using System.Threading.Tasks;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;

namespace MP.MKKing.Core.Interfaces
{
    /// <summary>
    /// Interface for the generic repository <see href="BaseRepository"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id); 
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}