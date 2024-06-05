using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Movie
{
	public class MovieUpdateViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[MovieDescription]
		public string Description { get; set; }

		[ReleaseYear]
		[Display(Name = "Год выхода фильма")]
		public int ReleaseYear { get; set; }

		[Required(ErrorMessage = "Не заполнено имя режиссера")]
		[ValidSymbols]
		[Display(Name = "Имя режиссера")]
		public string Director { get; set; }

		public int Budget { get; set; }

		[Required(ErrorMessage = "Не заполнена страна производства")]
		[ValidSymbols]
		[Display(Name = "Страна производства")]
		public string CountryOfOrigin { get; set; }
	}
}
