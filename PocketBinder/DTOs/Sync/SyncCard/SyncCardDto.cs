namespace PocketBinder.DTOs.Sync.SyncCard
{
    public class SyncCardDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Supertype { get; set; }
        public List<string>? Subtypes { get; set; }
        public List<string>? Types { get; set; }
        public string? Rarity { get; set; }
        public string Number { get; set; }
        public string? Artist { get; set; }
        public SyncCardImagesDto? Images { get; set; }
        public string SetId { get; set; }
        public SyncTcgPlayerDto? TcgPlayer { get; set; }
        public SyncCardMarketDto? CardMarket { get; set; }
    }
}
