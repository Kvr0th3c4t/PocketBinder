using PocketBinder.Enums;

namespace PocketBinder.DTOs.Album
{
    public class AlbumSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string SetName { get; set; } = string.Empty;
        public int TotalCards { get; set; }
        public int OwnedCards { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
