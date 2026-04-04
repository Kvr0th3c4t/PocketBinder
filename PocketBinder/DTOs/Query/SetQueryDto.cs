namespace PocketBinder.DTOs.Query
{
    public class SetQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string ReleaseDateFrom { get; set; } = string.Empty;
        public string ReleaseDateTo { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
