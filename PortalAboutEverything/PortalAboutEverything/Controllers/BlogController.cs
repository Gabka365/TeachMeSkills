using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
                .GetAll()
                .Select(BuildPostIndexViewModel)
                .ToList();

            return View(postsViewModel);
        }


        [HttpGet]
        public IActionResult PostMessage()
        {
            var now = DateTime.Now;
            var name = "Morgan Freeman";

            var viewModel = new BlogIndexViewModel
            {
                Now = now,
                Name = name,
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult DeleteMessage(int id)
        {
            _posts.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult CreatePost(ReceivingDataViewModel viewModel)
        {
            var NewPost = new Post
            {
                Name = viewModel.Name,
                message = viewModel.message,
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
        public IActionResult UpdatePost(postUpdateViewModel viewModel)
        {
            var Post = new Post
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                message = viewModel.message,
                Now = viewModel.Now
            };

            _posts.Update(Post);

            return RedirectToAction("Index");
        }


        public IActionResult SendingMessage()
        {
            var now = DateTime.Now;
            var name = "Morgan Freeman";

            var viewModel = new BlogIndexViewModel
            {
                Now = now,
                Name = name,
            };

            return View(viewModel);
        }


        public IActionResult ReceivingMessage(ReceivingDataViewModel viewModel)
        {
            return View(viewModel);
        }


        private postIndexViewModel BuildPostIndexViewModel(Post post)
        {
            return new postIndexViewModel
            {
                Id = post.Id,
                message = post.message,
                Now = post.Now,
                Name = post.Name,
            };
        }


        private postUpdateViewModel BuildPostUpdateViewModel(Post post)
        {
            return new postUpdateViewModel
            {
                Id = post.Id,
                message = post.message,
                Now = post.Now,
                Name = post.Name,
            };
        }
    }
}
