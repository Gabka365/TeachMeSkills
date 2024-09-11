
namespace PortalAboutEverything.Data.Model
{
    public class Traveling : BaseModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string TimeOfCreation { get; set; }
        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

    }
}
