using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;
using PortalAboutEverything.Controllers;
using PortalAboutEverything.Data.Model;

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

        public List<PostIndexViewModel> GetAll()
        {
            var postsViewModel = _blogRepositories
                .GetAllWithCommentsBlog()
                .Select(BuildPostIndexViewModel)
                .ToList();
            return postsViewModel;
        }

        public int? GetLikes(int id)
        {
            UpdateLikes(id);
            return _blogRepositories.GetLikeCountByPostId(id);
        }

        public int? GetDislikes(int id)
        {
            UpdateDislikes(id);
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

        private PostIndexViewModel BuildPostIndexViewModel(Post post)
        {
            return new PostIndexViewModel
            {
                Id = post.Id,
                Message = post.Message,
                DateOfPublish = post.CurrentTime.ToString(),
                Name = post.Name,
            };
        }
    }
}
