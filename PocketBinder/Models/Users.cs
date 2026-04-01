using PocketBinder.Enums;

namespace PocketBinder.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public PlanType PlanType { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<AlbumCards>? AlbumCards { get; set; }
        public ICollection<Albums>? Albums { get; set; }
    }
}
