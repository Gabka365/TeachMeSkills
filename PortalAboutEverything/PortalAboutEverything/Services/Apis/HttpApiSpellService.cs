using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpApiSpellService
    {
        private HttpClient _httpClient;

        public HttpApiSpellService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SpellDto> GetSpellAsync()
        {
            var response = await _httpClient.GetAsync("/en/spells/random");
            var dtos = await response.Content.ReadFromJsonAsync<SpellDto>();
            return dtos;
        }
    }
}
