using BlogApi.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Data.Repositories
{
    public class BlogApiRepositories
    {
        protected BlogApiDbContext _dbContext;
        protected DbSet<CommentBlog> _dbSet;

        public BlogApiRepositories(BlogApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<CommentBlog>();
        }


        public void AddComment(string userName, string commentText, string time,int postId)
        {
            var comment = new CommentBlog
            {
                PostId = postId,
                Message = commentText,
                Name = userName,
                CurrentTime = time,
            };

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public List<CommentBlog> GetAllComments()
        {
            return _dbSet.ToList();
        }

    }
}
