using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using ProfileService.Infrastructure.Data;

namespace ProfileService.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ProfileDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ProfileDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await _dbSet.CountAsync(criteria, cancellationToken);
        }

        public IQueryable<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> Filters,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool asNoTracking = true,
            bool distinct = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (Filters != null)
            {
                query = query.Where(Filters);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (distinct)
            {
                query = query.Distinct();
            }

            return query;
        }

        public EntityEntry<TEntity> PartialUpdate(TEntity entity, params string[] propertyNames)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            foreach (var propertyName in propertyNames)
            {
                entry.Property(propertyName).IsModified = true;
            }

            return entry;
        }
    }
}