using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Infrastructure.Data.Contexts;

namespace WorkoutService.Infrastructure.Data.Repositories
{
    public class GenericRepository<T>(WorkoutDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly WorkoutDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> GetAll() => _dbSet.AsQueryable();

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
        }

        public async Task<T?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct)
        {
            return await _dbSet.AsTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void SaveInclude(T entity, params string[] includeProperties)
        {
            var entityId = (Guid)entity.GetType().GetProperty("Id")!.GetValue(entity)!;
            var localEntity = _context.Set<T>().Local.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")!.GetValue(e)! == entityId);

            EntityEntry<T> entry = localEntity == null ? _context.Set<T>().Attach(entity) : _context.Entry(localEntity);

            foreach (var includeProperty in includeProperties)
            {
                var value = entity.GetType().GetProperty(includeProperty)?.GetValue(entity);
                entry.Property(includeProperty).CurrentValue = value;
                entry.Property(includeProperty).IsModified = true;
            }
        }

        public void Delete(T entity)
        {
            if (entity is WorkoutPlan workoutPlan)
            {
                var localEntity = _context.WorkoutPlans.Local.FirstOrDefault(e => e.Id == workoutPlan.Id);
                EntityEntry<WorkoutPlan> entry = localEntity == null ? _context.WorkoutPlans.Attach(workoutPlan) : _context.Entry(localEntity);

                workoutPlan.MarkAsDeleted();

                entry.Property(x => x.IsDeleted).IsModified = true;
                entry.Property(x => x.DeletedAt).IsModified = true;
            }
            else
            {
                var entityId = (Guid)entity.GetType().GetProperty("Id")!.GetValue(entity)!;
                var localEntity = _context.Set<T>().Local.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")!.GetValue(e)! == entityId);

                if (localEntity != null)
                {
                    _dbSet.Remove(localEntity);
                }
                else
                {
                    _dbSet.Remove(entity);
                }
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
