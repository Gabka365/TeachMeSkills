using PortalAboutEverything.Models.ValidationAttributes;
namespace PortalAboutEverything.Models.GameStore
{
    public class CreateGameStoreViewModel
    {
        public string GameName { get; set; }
        public string Developer { get; set; }

        [ComputerGameYear]
        public int YearOfRelease { get; set; }
    }
}
