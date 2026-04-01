using PocketBinder.Enums;

namespace PocketBinder.Models
{
    public class Albums
    {
        public int AlbumId { get; set; }
        public int UserId { get; set; }
        public string AlbumName { get; set; } = string.Empty;
        public AlbumType AlbumType { get; set; }
        public string SetId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<AlbumCards>? AlbumCards { get; set; }
    }
}
