using PocketBinder.Enums;

namespace PocketBinder.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public PlanType PlanType { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserCollection>? UserCollection { get; set; }
        public ICollection<Album>? Albums { get; set; }
    }
}
