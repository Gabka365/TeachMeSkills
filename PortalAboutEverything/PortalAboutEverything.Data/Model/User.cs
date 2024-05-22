namespace PortalAboutEverything.Data.Model
{
    public class User : BaseModel
    {
        // TODO [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserName { get; set; }
        public string Password { get; set; } // TODO Store only hash

        public virtual List<Game> FavoriteGames { get; set; }
    }
}
