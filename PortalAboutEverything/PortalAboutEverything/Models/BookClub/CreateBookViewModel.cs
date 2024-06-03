

using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PortalAboutEverything.Models.BookClub
{
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "Имя автора не может быть пустым")]
        public required string BookAuthor { get; set; }

        [Required(ErrorMessage = "Название книги не может быть пустым")]
        public required string BookTitle { get; set; }

        [Required(ErrorMessage = "Описание книги не может быть пустым")]
        public required string SummaryOfBook { get; set; }

        [BookPublicationYear(2023, ErrorMessage = "Годы публикации не может быть меньше 2000")]
        [Display(Name = "год публикации книги")]
        public int YearOfPublication { get; set; }
    }
}
