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
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
