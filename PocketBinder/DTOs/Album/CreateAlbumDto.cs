using PocketBinder.Enums;

namespace PocketBinder.DTOs.Album
{
    public class CreateAlbumDto
    {
        public string Name { get; set; } = string.Empty;
        public AlbumType Type { get; set; }
        public string? SetId { get; set; }
    }
}
