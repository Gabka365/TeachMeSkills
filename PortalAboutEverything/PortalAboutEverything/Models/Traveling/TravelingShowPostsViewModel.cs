using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingShowPostsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string TimeOfCreation { get; set; }
        public string? NameImage { get; set; }
        public int UserId { get; set; }
        public List<TravelingCreateComment>? Comments { get; set; }

    }
}
