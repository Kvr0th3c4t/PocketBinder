namespace PocketBinder.Models
{
    public class UserCollection
    {
        public int UserCollectionId { get; set; }
        public int UserId { get; set; }
        public string CardId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }

        //Foreign key relationships
        public User? User { get; set; }
    }
}
