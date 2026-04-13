using PocketBinder.Enums;

namespace PocketBinder.DTOs.Album
{
    public class AlbumDetailDto
    {
        public int Id    { get; set; }
        public string Name  { get; set; } = string.Empty;
        public string Type  { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string SetName { get; set; } = string.Empty;
        public List<AlbumCardDto> Cards { get; set; } = new List<AlbumCardDto>();
    }
}
