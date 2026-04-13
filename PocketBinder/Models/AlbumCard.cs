namespace PocketBinder.Models
{
    public class AlbumCard
    {
        public int AlbumCardId { get; set; }
        public int AlbumId { get; set; }
        public string CardId { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
        //Foreign key relationships
        public Album? Album { get; set; }
        public Card? Card { get; set; }
    }
}
