using Microsoft.EntityFrameworkCore;

namespace NutritionService.Common
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
           this IQueryable<T> query,
           int pageIndex,
           int pageSize)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}
