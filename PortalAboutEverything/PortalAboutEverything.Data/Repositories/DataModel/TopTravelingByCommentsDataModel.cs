using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories.DataModel
{
    public class TopTravelingByCommentsDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string TimeOfCreation { get; set; }
        public int UserId { get; set; }
        public int CommentCount { get; set; }
    }
}
