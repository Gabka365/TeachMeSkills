namespace PortalAboutEverything.Data.Model
{
    public class Game : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int YearOfRelease { get; set; }

        public virtual List<BoardGameReview> Reviews { get; set; }
        
        public virtual List<User> UserWhoFavoriteTheGame { get; set; }
    }
}
