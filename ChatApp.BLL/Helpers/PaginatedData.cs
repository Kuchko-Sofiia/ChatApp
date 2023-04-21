
using Microsoft.EntityFrameworkCore;

namespace ChatApp.BLL.Models
{
    public class PaginatedData<T> : List<T>
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int PageIndex { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedData() { }
        public PaginatedData(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static async Task<PaginatedData<T>> GetPaginatedDataAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedData<T>(items, count, pageIndex, pageSize);
        }
    }
}
