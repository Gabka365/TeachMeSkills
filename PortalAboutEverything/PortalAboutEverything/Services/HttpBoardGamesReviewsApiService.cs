using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services
{
    public class HttpBoardGamesReviewsApiService
    {
        private readonly HttpClient _httpClient;

        public HttpBoardGamesReviewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void CreateReview(DtoBoardGameReview review)
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
    }
}
