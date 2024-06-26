using Microsoft.EntityFrameworkCore;
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

        public string LastNews()
        {
            var lastNews = _dbContext.News.OrderByDescending(n => n.Id).FirstOrDefault();
            return lastNews!.Text;
        }           
    }
}
