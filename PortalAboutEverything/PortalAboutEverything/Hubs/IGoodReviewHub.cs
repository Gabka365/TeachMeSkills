namespace PortalAboutEverything.Hubs
{
    public interface IGoodReviewHub
    {
        Task NotifyAboutNewReview(int goodId, string review, string userName);
    }
}
