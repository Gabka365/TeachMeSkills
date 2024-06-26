namespace PortalAboutEverything.Hubs
{
    public interface IMovieHub
    {
        Task NotifyAboutDelitingMovie(string userName, string movieName);
    }
}
