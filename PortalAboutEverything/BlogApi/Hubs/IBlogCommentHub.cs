namespace BlogApi.Hubs
{
    public interface IBlogCommentHub
    {
        Task NotifyAboutNewComment(string userName, string text, string formattedTime, int postId);
    }
}
