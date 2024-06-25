using BoardGamesRiviewsApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesRiviewsApi.Data
{
    public class ReviewsDbContext : DbContext 
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\mssqllocaldb;Integrated Security=True;Database=BoardGamesReviewsApi";
        public DbSet<BoardGameReview> BoardGameReviews { get; set; }

        public ReviewsDbContext() { }
        public ReviewsDbContext(DbContextOptions<ReviewsDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }

    }
}
