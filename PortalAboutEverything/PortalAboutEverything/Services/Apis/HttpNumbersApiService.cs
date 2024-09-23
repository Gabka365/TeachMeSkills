namespace PortalAboutEverything.Services.Apis
{
    public class HttpNumbersApiService
    {
        private HttpClient _httpClient;

        public HttpNumbersApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFactAsync()
        {
            
            var response = await _httpClient.GetAsync($"/{DateTime.Now.Month}/{DateTime.Now.Day}/date");
            var fact = await response.Content.ReadAsStringAsync();

            return fact;
        }
    }
}
