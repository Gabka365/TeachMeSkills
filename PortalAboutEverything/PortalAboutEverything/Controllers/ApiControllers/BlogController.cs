using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;
using PortalAboutEverything.Controllers;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Models.Game;

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

        [HttpPost]
        public bool Create(PostUpdateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return false;
            }

            var postDb = new Post
            {
                Id = model.Id,
                Message = model.message,
                Name = model.Name,
                CurrentTime = DateTime.Now
            };


            _blogRepositories.Create(postDb);

            return true;
        }


        [HttpPost]
        public bool Update(PostUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var postDb = new Post
            {
                Id = model.Id,
                Message = model.message,
                Name = model.Name,
                CurrentTime = DateTime.Now
            };


            _blogRepositories.Update(postDb);

            return true;
        }

            
        public void Remove(int id)
        {
            _blogRepositories.Delete(id);
        }

        public PostIndexViewModel Get(int id)
            => BuildPostIndexViewModel(_blogRepositories.Get(id));

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
