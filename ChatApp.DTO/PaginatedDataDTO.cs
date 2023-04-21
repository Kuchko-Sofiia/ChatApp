namespace ChatApp.DTO
{
    public class PaginatedDataDTO<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;
        public List<T> Items { get; set; } = new();
        public PaginatedDataDTO() { }
    }
}
