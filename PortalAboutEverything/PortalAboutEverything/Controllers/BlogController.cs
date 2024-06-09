using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;


namespace PortalAboutEverything.Controllers
{
    public class BlogController : Controller
    {
        private BlogRepositories _posts;

        public BlogController(BlogRepositories posts)
        {
            _posts = posts;
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
        public IActionResult PostMessage()
        {
            var viewModel = BuildBlogIndexViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult DeleteMessage(int id)
        {
            _posts.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult CreatePost(MessageReceiveViewModel viewModel)
        {
            var NewPost = new Post
            {
                Name = viewModel.Name,
                Message = viewModel.Message,
                Now = viewModel.Now
            };

            _posts.Create(NewPost);

            return RedirectToAction("Index");
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
            var viewModel = BuildBlogIndexViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ReceiveMessage(MessageReceiveViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddComment(WriteBlogComment viewModel)
        {
            _posts.AddComment(viewModel.postId, viewModel.Text);
            return RedirectToAction("Index");
        }

        private MessageMetadataViewModel BuildBlogIndexViewModel()
            => new MessageMetadataViewModel
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
