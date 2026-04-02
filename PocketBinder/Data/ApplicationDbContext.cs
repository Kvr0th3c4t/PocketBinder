using Microsoft.EntityFrameworkCore;
using PocketBinder.Models;

namespace PocketBinder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AlbumCard> AlbumCards { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }

}