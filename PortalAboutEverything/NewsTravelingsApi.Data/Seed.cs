using Microsoft.Extensions.DependencyInjection;
using NewsTravelingsApi.Data.Model;
using NewsTravelingsApi.Data.Repositories;

namespace NewsTravelingsApi.Data
{
    public class Seed
    {
        public void Fill(IServiceProvider serviceProvider)
        {
            using var service = serviceProvider.CreateScope();

            FillNews(service);
        }
        private void FillNews(IServiceScope service)
        {
            var newsRepository = service.ServiceProvider.GetService<NewsRepository>()!;
            if (!newsRepository.Any())
            {
                var firstNews = new News
                {
                    UserId = 0,
                    Text = "Лето наступило, и многие уже задумываются о том, " +
                    "чтобы отправиться в отпуск. Некоторые, конечно, планировали свои путешествия еще зимой, " +
                    "но в туристической отрасли есть и другая практика"
                };
               newsRepository.Create(firstNews);
            }            
        }
    }
}
