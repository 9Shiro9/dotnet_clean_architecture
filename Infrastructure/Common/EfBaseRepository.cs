using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
    public class EfBaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public EfBaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Insert
        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }
        public void Remove(object Id)
        {
            var _deleteEntity = _dbContext.Set<T>().Find(Id);

            if (_deleteEntity != null)
            {
                _dbContext.Remove(_deleteEntity);
            }
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
        #endregion

        #region Select
        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(string includeString)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                string[] includeStrings = includeString.Split(",");

                foreach (string include in includeStrings)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(List<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate, string includeString)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                string[] includeStrings = includeString.Split(",");

                foreach (string include in includeStrings)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null) query = query.Where(predicate);

            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeString, bool disableTracking)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                string[] includeStrings = includeString.Split(",");

                foreach (string include in includeStrings)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, List<Expression<Func<T, object>>> includes, bool disableTracking)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }
        public IQueryable Queryable()
        {
            return _dbContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(object Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
        public T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, string includeString)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                string[] includeStrings = includeString.Split(",");

                foreach (string include in includeStrings)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
        #endregion
    }
}
