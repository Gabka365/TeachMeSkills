using PortalAboutEverything.Models.Traveling;

namespace PortalAboutEverything.Hubs
{
    public interface ICommentTravelingHub
    {
        Task NotifyAboutNewComment(TravelingCreateComment travelingCreateComment);      
       

    }
}

