namespace PortalAboutEverything.Models.BoardGameReview
{
    public class BoardGameReviewViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Text { get; set; }
    }
}
