using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model.Store
{
    public class GoodReview : BaseModel
    {        
        public string Description { get; set; }

        public virtual Good? Good { get; set; }
    }
}
