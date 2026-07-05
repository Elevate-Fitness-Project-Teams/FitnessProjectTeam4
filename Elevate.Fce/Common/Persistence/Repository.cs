using Microsoft.EntityFrameworkCore;

namespace Elevate.Fce.Common.Persistence;

public class Repository : IRepository
{
    private readonly FceDbContext _db;
    public Repository(FceDbContext db) => _db = db;

    public IQueryable<T> Query<T>() where T : class => _db.Set<T>().AsQueryable();

    public Task<T?> GetAsync<T>(Guid id, CancellationToken ct) where T : class =>
        _db.Set<T>().FindAsync(new object[] { id }, ct).AsTask();

    public async Task AddAsync<T>(T entity, CancellationToken ct) where T : class =>
        await _db.Set<T>().AddAsync(entity, ct);

    public void Update<T>(T entity) where T : class => _db.Set<T>().Update(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
