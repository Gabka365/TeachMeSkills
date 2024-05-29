using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ProductCodeAttribute : ValidationAttribute
    {
        private string _errorMessage;

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage) ? _errorMessage : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not long)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(ProductCodeAttribute)} can work only with long");
            }

            long valueInLong = (long)value;
            if(valueInLong < 10000 || valueInLong > 99999)
            {
                _errorMessage = "Код товара должен быть положительным числом из 5 цифр";
                return false;
            }

            return true;
        }
    }
}
