using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FitnessCalculationEngine.Common.Persistence;

public class Repository : IRepository
{
    private readonly FceDbContext _db;
    public Repository(FceDbContext db) => _db = db;

    public IQueryable<T> Query<T>() where T : class => _db.Set<T>();
    public IQueryable<T> QueryNoTracking<T>() where T : class => _db.Set<T>().AsNoTracking();

    public Task<T?> GetAsync<T>(Guid id, CancellationToken ct) where T : class =>
        _db.Set<T>().FindAsync(new object[] { id }, ct).AsTask();

    public void Add<T>(T entity) where T : class => _db.Set<T>().Add(entity);
    public void Update<T>(T entity) where T : class => _db.Set<T>().Update(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct) =>
        _db.Database.BeginTransactionAsync(ct);

    public void ClearChangeTracker() => _db.ChangeTracker.Clear();
}
