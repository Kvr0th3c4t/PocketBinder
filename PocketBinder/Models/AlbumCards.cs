namespace PocketBinder.Models
{
    public class AlbumCards
    {
        public int AlbumCardId { get; set; }
        public int AlbumId { get; set; }
        public string CardId { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}
