using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model
{
    public class Like : BaseModel
    {
        public virtual List<Traveling> Travelings { get; set; }
        public virtual List<User> Users { get; set; }
    }
}


