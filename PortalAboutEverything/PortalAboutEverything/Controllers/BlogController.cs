using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;
using PortalAboutEverything.Services.AuthStuff;


namespace PortalAboutEverything.Controllers
{
    public class BlogController : Controller
    {
        private BlogRepositories _posts;
        private AuthService _authService;

        public BlogController(BlogRepositories posts, AuthService authService)
        {
            _posts = posts;
            _authService = authService;
        }

        public IActionResult Index()
        {

            var postsViewModel = _posts
                .GetAllWithCommentsBlog()
                .Select(BuildPostIndexViewModel)
                .ToList();

            return View(postsViewModel);
        }


        [HttpGet]
        [Authorize]
        [HasRole(Data.Enums.UserRole.BlogAdmin)]
        public IActionResult CreatePost()
        {
            var viewModel = BuildMessageViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult DeleteMessage(int id)
        {
            _posts.Delete(id);
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
                Now = viewModel.Now
            };

            _posts.Create(NewPost);

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
                Now = viewModel.Now
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

            _posts.AddComment(viewModel.postId, viewModel.Text);
            return RedirectToAction("Index");
        }

        private MessageViewModel BuildMessageViewModel()
            => new MessageViewModel
            {
                Now = DateTime.Now,
                Name = "Morgan Freeman"
            };

        private PostIndexViewModel BuildPostIndexViewModel(Post post)
        {
            return new PostIndexViewModel
            {
                Id = post.Id,
                Message = post.Message,
                Now = post.Now,
                Name = post.Name,
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
                Now = commentBlog.Now,
                Name = commentBlog.Name,
            };
        }

        private PostUpdateViewModel BuildPostUpdateViewModel(Post post)
        {
            return new PostUpdateViewModel
            {
                Id = post.Id,
                message = post.Message,
                Now = post.Now,
                Name = post.Name,
            };
        }
    }
}
