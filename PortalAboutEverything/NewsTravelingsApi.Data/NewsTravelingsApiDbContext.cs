using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using NewsTravelingsApi.Data.Model;

namespace NewsTravelingsApi.Data
{
    public class NewsTravelingsApiDbContext : DbContext
    {

        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=NewsTravelings";

        public DbSet<News> News { get; set; }

        public NewsTravelingsApiDbContext() { }
        public NewsTravelingsApiDbContext(DbContextOptions<NewsTravelingsApiDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }
    }
}
