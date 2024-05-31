using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model
{
    public class HistoryEvent : BaseModel
    {   
        public string Name { get; set; }

        public string Description { get; set; }

        public int YearOfEvent { get; set; }
        public List<User> UserWhoFavoriteTheHistoryEvent { get; set; }
    }
}
