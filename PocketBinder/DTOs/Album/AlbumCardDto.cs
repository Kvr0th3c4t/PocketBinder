namespace PocketBinder.DTOs.Album
{
    public class AlbumCardDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string SmallImageUrl { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public bool IsOwned { get; set; }
    }
}
