namespace ProgressService.Common.Behaviors
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalCount = count;
            if (pageSize > 0)
            {
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            }
        }
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
