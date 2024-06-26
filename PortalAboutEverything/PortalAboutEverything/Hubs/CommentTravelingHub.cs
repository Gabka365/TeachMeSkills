using Microsoft.AspNetCore.SignalR;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Traveling;
using PortalAboutEverything.Services.AuthStuff;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Hubs
{
    public class CommentTravelingHub : Hub<ICommentTravelingHub>
    {
        private TravelingRepositories _travelingRepositories;
        private CommentRepository _commentRepository;

        public CommentTravelingHub(TravelingRepositories travelingRepositories, CommentRepository commentRepository)
        {
            _travelingRepositories = travelingRepositories;
            _commentRepository = commentRepository;
        }

        public void AddNewComment(TravelingCreateComment travelingCreateComment)
        {
            var validationResults = new List<ValidationResult>();           
            var context = new ValidationContext(travelingCreateComment, null, null);
        
            if (!Validator.TryValidateObject(travelingCreateComment, context, validationResults, true))
            {                
                foreach (var validationResult in validationResults)
                {
                    travelingCreateComment.Text = validationResult.ErrorMessage!;
                    Clients.Caller.NotifyAboutNewComment(travelingCreateComment);
                    return;
                }
               
            }
            var traveling = _travelingRepositories.Get(travelingCreateComment.TravelingId);
            
            var comment = new Comment
            {
                Text = travelingCreateComment.Text,
                Traveling = traveling
            };

            _commentRepository.Create(comment);

            Clients.All.NotifyAboutNewComment(travelingCreateComment);           
        }
    }
}
