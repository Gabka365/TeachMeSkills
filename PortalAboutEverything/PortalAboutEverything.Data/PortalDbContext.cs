using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;

namespace PortalAboutEverything.Data
{
    public class PortalDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=Net16Portal";

        public DbSet<Game> Games { get; set; }
        public DbSet<GameStore> GameStores { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<VideoInfo> Videos { get; init; }
        public DbSet<BoardGameReview> BoardGameReviews { get; set; }

        public DbSet<History> History { get; set; }
        public PortalDbContext() { }
        public PortalDbContext(DbContextOptions<PortalDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
