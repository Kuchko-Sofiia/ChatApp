namespace ChatApp.Blazor.Data
{
    public class TableStateData
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SearchText { get; set; }
        public SortOptionsData? SortOptions { get; set; }
    }

    public class SortOptionsData
    {
        public string SortProperty { get; set; }
        public SortDirectionData SortDirection { get; set; }

        public SortOptionsData(SortDirectionData sortDirection, string sortProperty)
        {
            SortDirection = sortDirection;
            SortProperty = sortProperty;
        }
    }

    public enum SortDirectionData
    {
        Ascending,
        Descending
    }
}
