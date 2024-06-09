using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model
{
    public class Post
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? Name { get; set; } 

        public DateTime? Now { get; set; }
    }
}
