namespace PocketBinder.DTOs.Query
{
    public class CardQueryDto : PagedQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string Supertype { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
    }
}
