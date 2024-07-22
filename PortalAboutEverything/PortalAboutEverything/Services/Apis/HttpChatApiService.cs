using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpChatApiService
    {
        private HttpClient _httpClient;

        public HttpChatApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DtoMessageCount> GetMessageCountAsync()
        {
            // It's a bad solution. We just to harry up
            try
            {
                var httpResponse = await _httpClient.GetAsync("GetMessageCount");
                var dto = await httpResponse
                    .Content
                    .ReadFromJsonAsync<DtoMessageCount>();
                return dto;
            }
            catch
            {
                return new DtoMessageCount { MessageCount = -1 };
            }
        }
    }
}
