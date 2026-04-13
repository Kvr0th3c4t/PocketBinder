using PocketBinder.Enums;

namespace PocketBinder.DTOs.Album
{
    public class CreateAlbumDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? SetId { get; set; }
    }
}
