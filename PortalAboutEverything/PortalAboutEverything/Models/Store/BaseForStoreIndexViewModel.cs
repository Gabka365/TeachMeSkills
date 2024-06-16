namespace PortalAboutEverything.Models.Store
{
    public class BaseForStoreIndexViewModel
    {
        public List<GoodViewModel> Goods { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsStoreAdmin {  get; set; }
    }
}
