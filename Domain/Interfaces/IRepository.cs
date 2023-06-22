using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        #region Insert
        Task<T> AddAsync(T entity);
        void Add(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void AddRange(IEnumerable<T> entities);

        #endregion

        #region Update
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        #endregion

        #region Delete
        void RemoveRange(IEnumerable<T> entities);
        void Remove(T entity);
        #endregion

        #region Aggregate
        int Count(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Select
        Task<IReadOnlyList<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                string includeString = null,
                                bool disableTracking = true,
                                int pageNumber = 0,
                                int pageSize = 10);

        IQueryable<T> GetQuarriable(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(string includeString = null);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                string includeString = null,
                                bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        Task<T> GetByIdAsync(object id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string includeString = null);
        #endregion

        #region SaveChanges
        void SaveChanges();

        Task SaveChangesAsync();
        #endregion

        #region Dispose
        void Dispose();

        Task DisposeAsync();

        #endregion

        void BeginTransaction();

        Task BeginTransactionAsync();

        void CommitTransaction();

        Task CommitTransactionAsync();

        void RollbackTransaction();
        Task RollbackTransactionAsync();
    }
}
