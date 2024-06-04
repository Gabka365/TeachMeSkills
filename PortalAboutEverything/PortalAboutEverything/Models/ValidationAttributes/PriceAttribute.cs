using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class PriceAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            var errorMessage = "Цена не может быть отрицательной или равной 0";

            return string.IsNullOrEmpty(ErrorMessage) ? errorMessage : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is not double)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(PriceAttribute)} can work only with double");
            }

            double valueInDouble = (double)value;
            if (valueInDouble <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
