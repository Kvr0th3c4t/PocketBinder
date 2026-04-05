using Refit;

namespace PocketBinder.DTOs.Query
{
    public class CardQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string Supertype { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        [AliasAs("page")]
        public int Page { get; set; } = 1;
        [AliasAs("pageSize")]
        public int PageSize { get; set; } = 2;
        public string OrderBy { get; set; } = string.Empty;
    }
}
