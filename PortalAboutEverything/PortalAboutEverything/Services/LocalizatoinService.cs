using PortalAboutEverything.LocalizationResources.BoardGame;

namespace PortalAboutEverything.Services
{
    public class LocalizatoinService
    {
        public string GetLocalizedNewBoardGameAlert(string boardGameTitle) 
        {
            return string.Format(BoardGame_Index.AlertForNewBoardGame, boardGameTitle);
        }
    }
}
