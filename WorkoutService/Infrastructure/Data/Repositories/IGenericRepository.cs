using System.Linq.Expressions;

namespace WorkoutService.Infrastructure.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void SaveInclude(T entity, params string[] includeProperties);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
