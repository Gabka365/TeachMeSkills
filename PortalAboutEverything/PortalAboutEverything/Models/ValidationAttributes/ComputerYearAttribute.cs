using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ComputerYearAttribute : ValidationAttribute
    {
        private const int MIN_YEAR = 1960;

        private int _maxYear;

        public ComputerYearAttribute()
        {
            _maxYear = DateTime.Now.Year;
        }

        public ComputerYearAttribute(int maxYear)
        {
            _maxYear = maxYear;
        }

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"""Поле "{name}" не правильное. Год не может быть меньше чем {MIN_YEAR}. Только в это время придумали компы""";

            return string.IsNullOrEmpty(ErrorMessage)
                ? defaultErrorMessage
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ComputerYearAttribute)} can work only with int");
            }

            var year = (int)value;

            return year > MIN_YEAR && year <= _maxYear;
        }
    }
}
