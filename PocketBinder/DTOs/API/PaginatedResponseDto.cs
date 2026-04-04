namespace PocketBinder.DTOs.API
{
    public class PaginatedResponseDto<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
