namespace Elevate.Fce.Common.Persistence;

// One generic repo. Handlers inject IRepository and operate across all entities.
public interface IRepository
{
    IQueryable<T> Query<T>() where T : class;
    Task<T?> GetAsync<T>(Guid id, CancellationToken ct) where T : class;
    Task AddAsync<T>(T entity, CancellationToken ct) where T : class;
    void Update<T>(T entity) where T : class;
    Task<int> SaveChangesAsync(CancellationToken ct);
}
