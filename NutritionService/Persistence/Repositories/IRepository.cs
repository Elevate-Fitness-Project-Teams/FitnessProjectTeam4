using System.Linq.Expressions;

namespace NutritionService.Persistence.Repositories
{
    public interface IRepository<T> where T : class
    {
        //Task<T?> GetByIdAsync(Guid id);
        //Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Query();
        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
