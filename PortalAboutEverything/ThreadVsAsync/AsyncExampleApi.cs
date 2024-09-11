using System.Runtime.InteropServices.Marshalling;

namespace ThreadVsAsync
{
    public class AsyncExampleApi
    {
        // Async
        public async Task<string> CallCatApiAsync()
        {
            // Block 1
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.thecatapi.com/");
            var url = "v1/images/search?size=med&mime_types=jpg&format=json&has_breeds=true&order=RANDOM&page=0&limit=1";
            
            var taskToSendHttp /* Block 2 */ = await httpClient.GetAsync(url);
            var content = taskToSendHttp.Content.ReadAsStringAsync().Result;
            return content;
        }

        // Sync
        public void Tes()
        {
            var task = CallCatApiAsync();
            // task.Wait();
            var a = task.Result;
        }
    }
}
