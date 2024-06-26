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
            // It's a bad solution. We just to harry up
            try
            {
                return _httpClient
                    .GetAsync("GetMessageCount")
                    .Result!
                    .Content
                    .ReadFromJsonAsync<DtoMessageCount>()
                    .Result!;
            }
            catch
            {
                return new DtoMessageCount { MessageCount = -1 };
            }
        }
    }
}
