namespace PortalAboutEverything.Data.Model
{
    public class GameStore : BaseModel
    {
        public string GameName { get; set; }
        public string Developer { get; set; }
        public int YearOfRelease { get; set; }

        public virtual List<User> UserTheGame { get; set; }
    }
}
