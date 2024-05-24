
namespace PortalAboutEverything.Data.Model
{
    public class Traveling : BaseModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string TimeOfCreation { get; set; }
        public virtual User User { get; set; }
    }
}
