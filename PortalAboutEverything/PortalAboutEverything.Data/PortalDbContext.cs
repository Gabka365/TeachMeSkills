using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.BookClub;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Model.VideoLibrary;


namespace PortalAboutEverything.Data
{
    public class PortalDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=Net16Portal";

        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameStore> GameStores { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CommentBlog> CommentsBlog { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DbSet<GoodReview> GoodReviews { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BoardGameReview> BoardGameReviews { get; set; }
        public DbSet<Traveling> Travelings { get; set; }
        public DbSet<History> HistoryEvents { get; set; }
        public DbSet<Video> Videos { get; init; }
        public DbSet<Folder> Folders { get; init; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
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

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Book>()
                .HasMany(x => x.BookReviews)
                .WithOne(x => x.Book)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.CommentsBlog)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Good>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Good)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteGames)
                .WithMany(x => x.UserWhoFavoriteTheGame);

            modelBuilder.Entity<Folder>()
                .HasMany(folder => folder.Videos)
                .WithOne(video => video.Folder)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteBoardsGames)
                .WithMany(x => x.UsersWhoFavoriteThisBoardGame);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteBooksOfUser)
                .WithMany(x => x.UsersWhoAddBookToFavorites);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Travelings)
                .WithOne(x => x.User);

            modelBuilder.Entity<Traveling>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Traveling);

            modelBuilder.Entity<GameStore>()
                .HasMany(x => x.UserTheGame)
                .WithMany(x => x.MyGames);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavouriteGoods)
                .WithMany(x => x.UsersWhoLikedTheGood);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteMovies)
                .WithMany(x => x.UsersWhoFavoriteTheMovie);

            modelBuilder.Entity<Like>()
                .HasMany(x => x.Users)
                .WithMany(x => x.Likes);

            modelBuilder.Entity<Like>()
                .HasMany(x => x.Travelings)
                .WithMany(x => x.Likes);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts)
                .WithMany(x => x.Users);

            base.OnModelCreating(modelBuilder);
        }
    }
}
