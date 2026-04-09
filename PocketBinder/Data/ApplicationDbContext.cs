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
        public DbSet<Set> Sets { get; set; }
        public DbSet<Card> Cards { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // Configuración de índices únicos para garantizar la integridad de los datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de índices únicos
            // Un usuario no puede tener la misma carta más de una vez en su colección
            modelBuilder.Entity<UserCollection>()
                .HasIndex(x => new { x.UserId, x.CardId })
                .IsUnique();
            // Un álbum no puede tener la misma carta más de una vez
            modelBuilder.Entity<Album>()
                .HasIndex(x => x.UserId);
            modelBuilder.Entity<AlbumCard>()
                .HasIndex(x => new { x.AlbumId, x.CardId })
                .IsUnique();
            // Un usuario no puede tener el mismo email o username más de una vez
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
            // Un usuario no puede tener el mismo email o username más de una vez
            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }
    }

}