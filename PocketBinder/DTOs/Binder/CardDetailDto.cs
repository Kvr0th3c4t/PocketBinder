namespace PocketBinder.DTOs.Binder
{
    public class CardDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string SetName { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string SmallImageUrl { get; set; } = string.Empty;
        public string LargeImageUrl { get; set; } = string.Empty;
        public string Supertype { get; set; } = string.Empty;
        public List<string>? Subtypes { get; set; }
        public List<string>? Types { get; set; }

        public string Artist { get; set; } = string.Empty;

        public string? TcgPlayerUrl { get; set; }
        public string? CardMarketUrl { get; set; }
    }
}
