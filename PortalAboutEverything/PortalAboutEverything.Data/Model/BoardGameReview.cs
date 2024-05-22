namespace PortalAboutEverything.Data.Model
{
    public class BoardGameReview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Text { get; set; }
        public virtual BoardGame? BoardGame { get; set; }

        public virtual Game? Game { get; set; }
    }
}
