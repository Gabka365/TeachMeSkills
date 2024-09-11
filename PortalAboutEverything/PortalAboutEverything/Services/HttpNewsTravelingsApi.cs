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
        public async Task<DtoLastNews> GetLastNewsAsync()
        {
            var response = await _httpClient.GetAsync("DtoLastNews");
            response.EnsureSuccessStatusCode();
            var lastNews = await response.Content.ReadFromJsonAsync<DtoLastNews>();
            return lastNews!;
        }
        public int GeLastNewsId()
        {
            var lastNews = GetLastNewsAsync().Result;
            return lastNews.Id;
        }
       
    }
}
