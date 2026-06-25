using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ProfileService.Infrastructure.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        public TEntity Add(TEntity entity);
        public void Delete(TEntity entity);
        public IQueryable<TEntity> GetAsync(Expression<Func<TEntity, bool>> Filters,
                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                           bool asNoTracking = true,
                           bool distinct = false);
        public EntityEntry<TEntity> PartialUpdate(TEntity entity, params string[] propertyNames);
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    }
}
