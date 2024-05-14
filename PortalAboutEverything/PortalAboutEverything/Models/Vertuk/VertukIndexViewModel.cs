namespace PortalAboutEverything.Models.Vertuk
{
    public class VertukIndexViewModel
    {
        public int Day{  get; set; }

        public string Month { get; set; }
        
        public int Year { get; set; }

        public int NewDay => DateTime.Now.AddDays(-10).Day;
    }
}
