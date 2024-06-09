using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ProductCodeAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            var errorMessage = "Код товара должен быть положительным числом из 5 цифр";

            return string.IsNullOrEmpty(ErrorMessage) ? errorMessage : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is not long)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ProductCodeAttribute)} can work only with long");
            }

            long valueInLong = (long)value;
            if (valueInLong < 10000 || valueInLong > 99999)
            {
                return false;
            }

            return true;
        }
    }
}