using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Common.Persistence;

public interface IRepository
{
    IQueryable<T> Query<T>() where T : class;
    IQueryable<T> QueryNoTracking<T>() where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    Task<int> SaveChangesAsync(CancellationToken ct);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct);
    Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel, CancellationToken ct);
    void ClearChangeTracker();
}
