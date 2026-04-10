namespace PocketBinder.DTOs.Query
{
    public class PagedQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string OrderBy { get; set; } = string.Empty;
    }
}
