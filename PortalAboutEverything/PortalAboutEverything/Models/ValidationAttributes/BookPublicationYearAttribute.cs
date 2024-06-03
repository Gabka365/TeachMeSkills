using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class BookPublicationYearAttribute : ValidationAttribute
    {

        private const int MIN_YEAR = 2000;
        private int _maxYear;

        public BookPublicationYearAttribute()
        {
            _maxYear = DateTime.Now.Year;
        }
        public BookPublicationYearAttribute(int maxYear)
        {
            _maxYear = maxYear;
        }

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"Поле {name} заполнено неправильно. Год не может быть меньше чем {MIN_YEAR}";
            return string.IsNullOrEmpty(ErrorMessage)
                ? defaultErrorMessage
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                throw new ArgumentException($"Wrong type of validation {nameof(BookPublicationYearAttribute)}. Year must be number");
            }

            var year = (int)value;

            return year > MIN_YEAR && year <= _maxYear;
        }

    }
}
