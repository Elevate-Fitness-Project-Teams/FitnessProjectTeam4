using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NutritionService.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NutritionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(NutritionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //public async Task<T?> GetByIdAsync(Guid id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.AsNoTracking().ToListAsync();
        //}

        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
        }

        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        //}
    }
}
