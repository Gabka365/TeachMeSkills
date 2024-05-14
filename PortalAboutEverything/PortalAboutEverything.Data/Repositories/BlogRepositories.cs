using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BlogRepositories
    {
        private int _postID = 1;

        private List<Post> _posts = new List<Post>();

        public List<Post> GetAll()
        {
            return _posts.ToList(); 
        }

        public void Create(Post post)
        {
            post.Id = _postID++;
            _posts.Add(post);
        }

        public void Delete(int id)
        {
            var post = _posts.Single(x => x.Id == id);
            _posts.Remove(post);
        }


        public Post Get(int id)
        {
            return _posts.Single(x => x.Id == id);
        }

        public void Update(Post post)
        {
            Delete(post.Id);
            Create(post);
        }

    }
}
