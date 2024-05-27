using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Model.BookClub
{
	public class BookReview: BaseModel
    {
		public DateTime Date { get; set; }
		public string UserName { get; set; }
		public int BookRating { get; set; } = 0;
		public int BookPrintRating { get; set; } = 0;
		public int BookIllustrationsRating { get; set; } = 0;
		public string Text { get; set; }
		public virtual Book Book { get; set; }
	}
}
