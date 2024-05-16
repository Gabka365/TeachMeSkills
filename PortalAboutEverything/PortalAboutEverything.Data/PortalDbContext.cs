using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data
{
    public class PortalDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=Net16Portal";

        public DbSet<Game> Games { get; set; }
        public DbSet<GameStore> GameStores { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BoardGameReview> BoardGameReviews { get; set; }

        public PortalDbContext() { }
        public PortalDbContext(DbContextOptions<PortalDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }
    }
}
