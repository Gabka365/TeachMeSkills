using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model.Store
{
    public class Good : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public virtual List<GoodReview>? Reviews { get; set; }
    }
}
