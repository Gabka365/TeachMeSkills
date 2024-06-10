namespace PortalAboutEverything.Models.History
{
    public class HistoryEventsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int YearOfEvent { get; set; }

        public bool IsHistoryEventAdmin { get; set; }
    }
}
