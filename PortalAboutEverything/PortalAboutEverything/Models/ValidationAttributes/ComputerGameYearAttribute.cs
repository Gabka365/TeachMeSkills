using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ComputerGameYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ComputerGameYearAttribute)} can work only with int");
            }

            var year = (int)value;

            return year > 1962 && year <= DateTime.Now.Year;
        }

    }
}
