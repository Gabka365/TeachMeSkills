using Microsoft.EntityFrameworkCore;
using MoviesReviewsApi.Data.Model;

namespace MoviesReviewsApi.Data
{
    public class MoviesReviewsDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=MoviesReviewApi";
        public DbSet<MovieReview> MovieReviews { get; set; }

        public MoviesReviewsDbContext() { }
        public MoviesReviewsDbContext(DbContextOptions<MoviesReviewsDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }
    }
}
