using BestBoardGameApi.Dtos;
using BoardGameOfDayApi.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpBoardGameOfDayServise
    {
        private readonly HttpClient _httpClient;

        public HttpBoardGameOfDayServise(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DtoBoardGameOfDay> GetBoardGameOfDayAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("/getBoardGameOfDay");
                var dto = await response
                    .Content
                    .ReadFromJsonAsync<DtoBoardGameOfDay>();
                return dto;
            }
            catch
            {
                return new DtoBoardGameOfDay() { Id = -1 };
            }
        }
    }
}
