using Refit;

namespace PocketBinder.DTOs.Query
{
    public class SetQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;

        [AliasAs("page")]
        public int Page { get; set; } = 1;
        [AliasAs("pageSize")]
        public int PageSize { get; set; } = 20;
        public string OrderBy { get; set; } = string.Empty;
    }
}
