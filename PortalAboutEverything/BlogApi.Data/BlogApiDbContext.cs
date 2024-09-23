using BlogApi.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogApiDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlogApi;Integrated Security=True;Database=BlogApi";

        public DbSet<CommentBlog> Comments { get; set; }

        public BlogApiDbContext() { }
        public BlogApiDbContext(DbContextOptions<BlogApiDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }
    }
}
