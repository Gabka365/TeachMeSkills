using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services
{
    public class HttpChatApiService
    {
        private HttpClient _httpClient;

        public HttpChatApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public DtoMessageCount GetMessageCount()
        {
            return _httpClient
                .GetAsync("GetMessageCount")
                .Result!
                .Content
                .ReadFromJsonAsync<DtoMessageCount>()
                .Result!;
        }
    }
}
