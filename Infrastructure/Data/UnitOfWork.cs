using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
          await  _dbContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public void DisposeContext()
        {
            _dbContext.Dispose();
        }

        public async Task DisposeContextAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public void RollbackTransaction()
        {
           _dbContext.Database.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public void SaveChanges(CancellationToken cancellationToken = default)
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}
