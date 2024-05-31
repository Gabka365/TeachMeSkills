namespace PortalAboutEverything.Models.Store
{
    public class GoodViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public List<AddGoodReviewViewModel> Reviews { get; set; }
    }
}
