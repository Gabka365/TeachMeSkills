using BlogApi.Data.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace BlogApi.Hubs
{
    public class BlogCommentHub : Hub<IBlogCommentHub>
    {
        private BlogApiRepositories _blogApiRepositories;
        
        public BlogCommentHub(BlogApiRepositories blogApiRepositories)
        {
            _blogApiRepositories = blogApiRepositories;
        }

        public void AddNewComment(string userName, string commentText, string postId)
        {
            try
            {
                int Id = Int32.Parse(postId);

                DateTime currentTime = DateTime.Now;

                string formattedTime = currentTime.ToString("M/d/yyyy h:mm:ss tt");

                _blogApiRepositories.AddComment(userName, commentText, formattedTime, Id);

                Clients.All.NotifyAboutNewComment(userName, commentText, formattedTime, Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddNewComment: {ex.Message}");
                throw;
            }
        }
    }
}
