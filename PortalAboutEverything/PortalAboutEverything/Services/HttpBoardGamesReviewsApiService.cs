using BoardGamesReviewsApi.Dtos;

namespace PortalAboutEverything.Services
{
    public class HttpBoardGamesReviewsApiService
    {
        private readonly HttpClient _httpClient;

        public HttpBoardGamesReviewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public DtoBoardGameReview GetReview(int id)
        {
            return _httpClient
                .GetAsync($"get?id={id}")
                .Result
                .Content
                .ReadFromJsonAsync<DtoBoardGameReview>()
                .Result!;
        }

        public void CreateReview(DtoBoardGameReviewCreate review)
        {
            _httpClient
               .PostAsJsonAsync("createReview", review)
               .Result
               .EnsureSuccessStatusCode();
        }

        public void UpdateReview(DtoBoardGameReviewUpdate review)
        {
            _httpClient
               .PostAsJsonAsync("updateReview", review)
               .Result
               .EnsureSuccessStatusCode();
        }
    }
}
