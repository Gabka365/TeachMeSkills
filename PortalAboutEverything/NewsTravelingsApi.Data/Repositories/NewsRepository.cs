using NewsTravelingsApi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsTravelingsApi.Data.Repositories
{
    public class NewsRepository : BaseRepository<News>
    {
        public NewsRepository(NewsTravelingsApiDbContext dbContext) : base(dbContext) { }
    }
}
