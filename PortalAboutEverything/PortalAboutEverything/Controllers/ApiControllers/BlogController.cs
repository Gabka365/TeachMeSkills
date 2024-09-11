using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Controllers.ApiControllers
{

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class BlogController : Controller
    {
        private BlogRepositories _blogRepositories { get; set; }
        
        public BlogController(BlogRepositories blogRepositories) 
        { 
            _blogRepositories = blogRepositories;
        }

        public int? GetLikes(int id)
        {
            return _blogRepositories.GetLikeCountByPostId(id);
        }

        public int? GetDislikes(int id)
        {
            return _blogRepositories.GetDislikeCountByPostId(id);
        }

        public void UpdateLikes(int id)
        {
            _blogRepositories.UpdateLikeCountByPostId(id);
        }

        public void UpdateDislikes(int id)
        {
            _blogRepositories.UpdateDislikeCountByPostId(id);
        }

    }
}
