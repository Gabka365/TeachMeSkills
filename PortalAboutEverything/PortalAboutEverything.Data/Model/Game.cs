namespace PortalAboutEverything.Data.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int YearOfRelease { get; set; }

        public virtual List<BoardGameReview> Reviews { get; set; }
    }
}
