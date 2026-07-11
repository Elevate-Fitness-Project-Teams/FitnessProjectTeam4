using Microsoft.EntityFrameworkCore;
using ProgressService.Infrastructure.Data.Contexts;
using System.Linq.Expressions;

namespace ProgressService.Infrastructure.Data.Repositories
{
    public class GenericRepository<T>(ProgressDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly ProgressDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> GetAll() => _dbSet.AsQueryable();

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
