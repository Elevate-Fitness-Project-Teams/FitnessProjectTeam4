namespace NutritionService.Common
{
    public record PagedResult<T>(
        IReadOnlyCollection<T> Items,
        int TotalCount,
        int PageIndex,
        int PageSize
    )
    {
      
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
