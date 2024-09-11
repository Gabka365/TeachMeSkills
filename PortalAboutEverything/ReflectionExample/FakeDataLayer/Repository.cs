namespace ReflectionExample.FakeDataLayer
{
    public class Repository
    {
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
