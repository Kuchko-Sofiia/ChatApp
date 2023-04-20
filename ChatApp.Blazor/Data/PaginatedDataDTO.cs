namespace ChatApp.Blazor.Data
{
    public class PaginatedDataDTO<T>
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int PageIndex { get; private set; }
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;
        public List<T> Items { get; set; } = new();
        public PaginatedDataDTO() { }
    }
}
