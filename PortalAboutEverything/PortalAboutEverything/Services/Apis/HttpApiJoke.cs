using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpApiJoke
    {
        private HttpClient _httpClient;

        public HttpApiJoke(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<JokeDto> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetAsync("/jokes/random");
            response.EnsureSuccessStatusCode();
            var joke = await response.Content.ReadFromJsonAsync<JokeDto>();
            return joke;
        }
    }
}
