using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ComputerMoodRateAttribute : ValidationAttribute
    {
        private const int MIN_RATE = 0;
        private const int MAX_RATE = 10;


        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"The field of {name} is wrong. The rate may be only between {MIN_RATE} and {MAX_RATE}";

            return string.IsNullOrEmpty(ErrorMessage)
                ? defaultErrorMessage
                : ErrorMessage;
        }


        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ComputerMoodRateAttribute)} can work only with int");
            }

            var rate = (int)value;

            return rate >= MIN_RATE && rate <= MAX_RATE;
        }
    }
}
