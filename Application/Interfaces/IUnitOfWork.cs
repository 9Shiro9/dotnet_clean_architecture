namespace Application.Interfaces
{
    public interface IUnitOfWork 
    {
        void SaveChanges(CancellationToken  cancellationToken = default);
        Task SaveChangesAsync(CancellationToken  cancellationToken = default);
        void DisposeContext();
        Task DisposeContextAsync();
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
    }
}
