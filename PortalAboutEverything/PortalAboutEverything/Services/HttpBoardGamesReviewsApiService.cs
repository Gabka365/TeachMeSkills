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
            //_httpClient
            //   .PostAsJsonAsync("createReview", review)
            //   .Result
            //   .EnsureSuccessStatusCode();

            var url = $"createReview?userName={review.UserName}&userId={review.UserId}&dateOfCreation={review.DateOfCreation}&text={review.Text}&boardGameId={review.BoardGameId}";
            var a = _httpClient
                .GetAsync(url)
                .Result
                .Content
                .ReadFromJsonAsync<bool>()
                .Result; 
        }

        public void UpdateReview(DtoBoardGameReviewUpdate review)
        {
            //_httpClient
            //   .PostAsJsonAsync("updateReview", review)
            //   .Result
            //   .EnsureSuccessStatusCode();

            var url = $"updateReview?id={review.Id}&text={review.Text}";
            var a = _httpClient
                .GetAsync(url)
                .Result
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;
        }
    }
}
