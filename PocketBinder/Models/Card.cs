using System.Collections.Generic;

namespace PocketBinder.Models
{
    public class Card
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Supertype { get; set; }
        public string? Subtypes { get; set; }
        public string? Types { get; set; }
        public string? Rarity { get; set; }
        public string Number { get; set; }
        public string? Artist { get; set; }
        public string? SmallImageUrl { get; set; }
        public string? LargeImageUrl { get; set; }
        public string SetId { get; set; }
        public string? TcgplayerUrl { get; set; }
        public string? CardMarketUrl { get; set; }

        public Set Set { get; set; }
        public ICollection<AlbumCard>? AlbumCards { get; set; }
    }
}
