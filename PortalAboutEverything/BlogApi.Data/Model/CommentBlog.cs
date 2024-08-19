using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Data.Model
{
    public class CommentBlog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CurrentTime { get; set; }
        public int PostId { get; set; }
    }
}
