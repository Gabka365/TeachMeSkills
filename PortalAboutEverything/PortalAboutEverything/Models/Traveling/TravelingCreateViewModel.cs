namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingCreateViewModel
    {

        public string Name { get; set; }

        public string Desc { get; set; }

        public string TimeOfCreation { get; set; } = DateTime.Now.ToString("dd MMM yyyy");

        public string? NameImage { get; set; }
    }
}
