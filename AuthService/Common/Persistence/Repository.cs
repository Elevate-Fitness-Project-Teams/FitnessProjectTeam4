using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Common.Persistence;

public class Repository(AuthDbContext db) : IRepository
{
    public IQueryable<T> Query<T>() where T : class => db.Set<T>();
    public IQueryable<T> QueryNoTracking<T>() where T : class => db.Set<T>().AsNoTracking();
    public void Add<T>(T entity) where T : class => db.Set<T>().Add(entity);
    public void Update<T>(T entity) where T : class => db.Set<T>().Update(entity);
    public Task<int> SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct) =>
        db.Database.BeginTransactionAsync(ct);
    public Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel, CancellationToken ct) =>
        db.Database.BeginTransactionAsync(isolationLevel, ct);
    public void ClearChangeTracker() => db.ChangeTracker.Clear();
}
