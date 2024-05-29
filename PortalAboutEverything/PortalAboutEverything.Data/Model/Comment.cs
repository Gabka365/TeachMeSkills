using PortalAboutEverything.Data.Model;


namespace PortalAboutEverything.Data.Model
{
    public class Comment : BaseModel
    {
       public string Text { get; set; }   
       public virtual Traveling Traveling { get; set; }
    }
}
