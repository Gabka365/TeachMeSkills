using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BlogRepositories
    {
        private PortalDbContext _dbContext;


        public BlogRepositories(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Post> GetAll()
        {
            return _dbContext.Posts.ToList();
        }

        public List<Post> GetAllWithCommentsBlog()
            => _dbContext
            .Posts
            .Include(x => x.CommentsBlog)
            .ToList();


        public void Create(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = _dbContext.Posts.Single(x => x.Id == id);
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

        public Post Get(int id)
        {
            return _dbContext.Posts.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Post post)
        {
            var db = Get(post.Id);

            db.Message = post.Message;

            _dbContext.SaveChanges();
        }

        public void AddComment(int postId, string text)
        {
            var post = Get(postId);

            var commentBlog = new CommentBlog
            {
                Message = text,
                Post = post,
                Now = DateTime.Now,
                Name = "Anonymous"
            };

            _dbContext.CommentsBlog.Add(commentBlog);
            _dbContext.SaveChanges();
        }
    }
}
