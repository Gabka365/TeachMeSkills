using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpMoviesAverageRateApiService
    {
        private HttpClient _httpClient;

        public HttpMoviesAverageRateApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DtoAverageRate>> GetAverageRatesAsync(List<int> moviesId)
        {

            var httpResponse = await _httpClient.PostAsJsonAsync("getAverageRateOfMovies", moviesId);
            var dtoList = await httpResponse
                .Content
                .ReadFromJsonAsync<List<DtoAverageRate>>();
            return dtoList;

        }
    }
}
