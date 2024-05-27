using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Models.BoardGame
{
    public class UserFavoriteBoardGamesViewModel
    {
        public string Name { get; set; }
        public List<BoardGameViewModel> FavoriteBoardGames { get; set; }
    }
}
