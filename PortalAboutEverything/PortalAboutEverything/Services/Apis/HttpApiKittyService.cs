using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpApiKittyService
    {
        private HttpClient _httpClient;

        public HttpApiKittyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KittyDto> GetKittyAsync()
        {
            var response = await _httpClient.GetAsync("/v1/images/search?size=med&mime_types=jpg&format=json&has_breeds=true&order=RANDOM&page=0&limit=1");
            var dtos = await response.Content.ReadFromJsonAsync<KittyDto[]>();
            return dtos[0];
        }
    }
}
