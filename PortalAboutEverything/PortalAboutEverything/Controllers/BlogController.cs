using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class BlogController : Controller
    {
        private BlogRepositories _posts;
        private AuthService _authService;
        private PortalAboutEverything.Services.PathHelper _pathHelper;

        public BlogController(BlogRepositories posts, AuthService authService, PortalAboutEverything.Services.PathHelper pathHelper)
        {
            _posts = posts;
            _authService = authService;
            _pathHelper = pathHelper;
        }

        public IActionResult Index()
        {
            BlogViewModel viewModel;

            
            if (_authService.IsAuthenticated())
            {
                var postsViewModel = _posts
                .GetAllWithCommentsBlog()
                .Select(BuildPostIndexViewModel)
                .ToList();
                    
                viewModel = new BlogViewModel()
                {
                    Name = _authService.GetUserName(),
                    Posts = postsViewModel,
                    IsAccessible = _authService.HasRoleOrHigher(UserRole.User),
                    UserLanguage = _authService.GetUserLanguage(),
                    Role = _authService.GetUserRole(),
                };
            }
            else
            {
                viewModel = new BlogViewModel()
                {
                    IsAccessible = false
                };
            }

            return View(viewModel);
        }


        [HttpGet]
        [Authorize]
        [HasRoleOrHigher(Data.Enums.UserRole.BlogAdmin)]
        public IActionResult CreatePost()
        {
            var viewModel = BuildMessageViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            _posts.Delete(id);

            var path = _pathHelper.GetPathToPostCover(id);
            System.IO.File.Delete(path);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult CreatePost(MessageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var NewPost = new Post
            {
                Name = viewModel.Name,
                Message = viewModel.Message,
                CurrentTime = DateTime.Now,
            };

            _posts.Create(NewPost);
            var path = _pathHelper.GetPathToPostCover(NewPost.Id);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                viewModel.Cover.CopyTo(fs);
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult Blogger()
        {
            var userName = _authService.GetUserName();

            var userId = _authService.GetUserId();
            var posts = _posts.GetPostsByUserId(userId);

            var viewModel = new BloggerViewModel
            {
                Name = userName,
                Posts = posts
                    .Select(BuildPostUpdateViewModel)
                    .ToList(),
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var post = _posts.Get(id);
            var viewModel = BuildPostUpdateViewModel(post);
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult UpdatePost(PostUpdateViewModel viewModel)
        {
            var Post = new Post
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Message = viewModel.message,
                CurrentTime = viewModel.CurrentTime
            };

            _posts.Update(Post);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            var viewModel = BuildMessageViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ReceiveMessage(MessageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SendMessage");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddComment(WriteBlogComment viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var name = _authService.GetUserName();  

            _posts.AddComment(viewModel.postId, viewModel.Text, name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [HasRoleOrHigher(Data.Enums.UserRole.BlogAdmin)]
        public IActionResult AddCover(BlogViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }


            var path = _pathHelper.GetPathToPostCover(viewModel.Id);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                viewModel.Cover.CopyTo(fs);
            }

            return RedirectToAction("Index");
        }

        private MessageViewModel BuildMessageViewModel()
            => new MessageViewModel
            {
                CurrentTime = DateTime.Now,
                Name = _authService.GetUserName(),
            };

        private PostIndexViewModel BuildPostIndexViewModel(Post post)
        {
            return new PostIndexViewModel
            {
                Id = post.Id,
                Message = post.Message,
                CurrentTime = post.CurrentTime,
                Name = post.Name,
                HasCover = _pathHelper.IsPostCoverExist(post.Id),
                CommentsBlog = post
                .CommentsBlog
                .Select(BuildBlogCommentViewModel)
                .ToList()
            };
        }

        private BlogCommentViewModel BuildBlogCommentViewModel(CommentBlog commentBlog)
        {
            return new BlogCommentViewModel
            {
                Message = commentBlog.Message,
                CurrentTime = commentBlog.CurrentTime,
                Name = commentBlog.Name,
            };
        }

        private PostUpdateViewModel BuildPostUpdateViewModel(Post post)
        {
            return new PostUpdateViewModel
            {
                Id = post.Id,
                message = post.Message,
                CurrentTime = post.CurrentTime,
                Name = post.Name,
            };
        }
    }
}
