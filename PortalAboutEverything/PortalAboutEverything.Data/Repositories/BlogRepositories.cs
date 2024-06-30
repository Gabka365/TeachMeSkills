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
    public class BlogRepositories : BaseRepository<Post>
    {
        public BlogRepositories(PortalDbContext db) : base(db) { }

        public List<Post> GetAllWithCommentsBlog()
            => _dbSet
            .Include(x => x.CommentsBlog)
            .ToList();

        public List<Post> GetPostsWithCommentsBlogByUserId(int userId)
            => _dbSet
            .Include(x => x.CommentsBlog)
            .Where(x => x.Users.Any(u => u.Id == userId))
            .ToList();


        public List<Post> GetPostsByUserId(int userId)
            => _dbSet
            .Where(x => x.Users.Any(u => u.Id == userId))
            .ToList();



        public void Update(Post post)
        {
            var db = Get(post.Id);

            db.Message = post.Message;

            _dbContext.SaveChanges();
        }


        public void AddComment(int postId, string text, string name)
        {
            var post = Get(postId);

            var comment = new CommentBlog
            {
                Message = text,
                Post = post,
                CurrentTime = DateTime.Now,
                Name = name
            };


            _dbContext.CommentsBlog.Add(comment);
            _dbContext.SaveChanges();
        }
    }
}
