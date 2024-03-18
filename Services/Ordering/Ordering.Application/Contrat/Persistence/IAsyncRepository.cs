

using Ordering.Domain.Commen;
using System.Linq.Expressions;

namespace Ordering.Application.Contrat.Persistence
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T> , IOrderedQueryable<T>> orderby = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                        List<Expression<Func<T,object>>> includes = null,
                                        bool disableTracking = true);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T  entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsynk(T entity);

    }
}
