using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {

        #region Insert
        Task AddAsync(T entity);
        void Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        #endregion

        #region Update
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        #endregion

        #region Delete
        void Remove(T entity);
        void Remove(object Id);
        void RemoveRange(IEnumerable<T> entities);
        #endregion

        Task<IReadOnlyList<T>> GetListAsync();
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate,string includeString);
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeString,bool disableTracking);
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, List<Expression<Func<T, object>>> includes, bool disableTracking);

        Task<T> GetByIdAsync(object Id);
        T GetById(object id);
        
        IQueryable Queryable();
    }
}
