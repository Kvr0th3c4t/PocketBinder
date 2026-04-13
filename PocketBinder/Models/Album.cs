using PocketBinder.Enums;

namespace PocketBinder.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int UserId { get; set; }
        public string AlbumName { get; set; } = string.Empty;
        public AlbumType AlbumType { get; set; }
        public string? SetId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        //Foreign key relationships
        public User? User { get; set; }
        public ICollection<AlbumCard>? AlbumCards { get; set; }
        public Set? Set { get; set; } // For easier access to set details when needed
    }
}
