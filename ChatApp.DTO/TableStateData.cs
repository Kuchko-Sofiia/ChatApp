namespace ChatApp.DTO
{
    public class TableStateData<T> where T : Enum
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SearchText { get; set; }
        public T? SortProperty { get; set; }
        public SortDirectionData? SortDirection { get; set; }
    }

    public enum SortDirectionData
    {
        None,
        Ascending,
        Descending
    }
}
