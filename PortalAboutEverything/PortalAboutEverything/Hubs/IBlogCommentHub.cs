namespace PortalAboutEverything.Hubs
{
    public interface IBlogCommentHub
    {
        Task NotifyAboutNewComment(string userName, string text, DateTime dateTime, int postId);
    }
}
