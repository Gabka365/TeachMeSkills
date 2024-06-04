using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ReleaseYearAttribute : ValidationAttribute
    {
        private const int MIN_YEAR = 1895;

        private int _maxYear;

        public ReleaseYearAttribute()
        {
            _maxYear = DateTime.Now.Year;
        }

        public ReleaseYearAttribute(int maxYear)
        {
            _maxYear = maxYear;
        }

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"""Поле "{name}" не правильное. Год должен быть больше чем {MIN_YEAR} и меньше чем {_maxYear}""";

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

            return year >= MIN_YEAR && year <= _maxYear;
        }
    }
}
