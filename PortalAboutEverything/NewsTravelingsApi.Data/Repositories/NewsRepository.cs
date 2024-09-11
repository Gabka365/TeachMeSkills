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

        public News LastNews()
        {
            var lastNews = _dbContext.News.OrderByDescending(n => n.Id).FirstOrDefault();
            return lastNews!;
        }
        public News GetNewsFromTextAndNewsId(string text, int newsId)
        {
           var news =  _dbContext.News.First( x => x.Text == text && x.Id == newsId);
            return news;
        }
    }
}
