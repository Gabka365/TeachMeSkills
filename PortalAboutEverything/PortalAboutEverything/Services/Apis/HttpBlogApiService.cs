using PortalAboutEverything.Services.Dtos;

namespace PortalAboutEverything.Services.Apis
{
    public class HttpBlogApiService
    {
        private HttpClient _httpClient;

        public HttpBlogApiService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task<List<CommentsDto>> GetAllCommentsByUsernameAsync(string username)
        {
            var response = await _httpClient.GetAsync($"GetAllUsersCommentsByUsername?username={username}");
            var commentsList = await response.Content.ReadFromJsonAsync<List<CommentsDto>>();
            return commentsList;
        }
    }
}
