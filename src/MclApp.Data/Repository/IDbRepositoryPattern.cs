using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;

namespace MclApp.Data.Repository
{
    public partial interface IDbRepositoryPattern<T> where T : BaseEntity
    {
        
        Task<T> GetById(object id);
        Task<OutResult> Insert(T entity);
        Task<OutResult> Insert(IEnumerable<T> entities);
        Task<OutResult> Update(T entity);
        Task<OutResult> Update(IEnumerable<T> entities);
        Task<OutResult> Delete(T entity);
        Task<OutResult> Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }

}
