using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ComputerGameYearAttribute : ValidationAttribute
    {
        private const int MIN_YEAR = 1962;
        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"""Поле "{name}" не правильное! Самая первая игра появилась в {MIN_YEAR} """;
            return string.IsNullOrEmpty(ErrorMessage)
                ? defaultErrorMessage
                : ErrorMessage;
        }
        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ComputerGameYearAttribute)} can work only with int");
            }

            var year = (int)value;

            return year > MIN_YEAR && year <= DateTime.Now.Year;
        }

    }
}
