using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories.DataModel
{
    public class Top3BoardGameDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountOfUserWhoLikeIt { get; set; }
    }
}
