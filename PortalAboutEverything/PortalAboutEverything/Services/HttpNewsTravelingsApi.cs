using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services
{
    public class HttpNewsTravelingsApi
    {
        private HttpClient _httpClient;
        
        public HttpNewsTravelingsApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }
        public DtoLastNews GetLastNews()
        {
            return _httpClient
                .GetAsync("DtoLastNews")
                .Result!
                .Content
                .ReadFromJsonAsync<DtoLastNews>()
                .Result!;           
        }
        public int GeLastNewsId()
        {
            var lastNews = GetLastNews();
            return lastNews.Id;
        }
    }
}
