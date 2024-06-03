using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class GoodNameAttribute : ValidationAttribute
    {
        private string _defaultErrorMessage;

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {            
            if (value == null)
            {
                _defaultErrorMessage = "Введите название товара.";
                return false;
            }

            if (value.ToString().Length < 5 || value.ToString().Length > 20)
            {
                _defaultErrorMessage = "Название товара должно быть не менее 5 и не боее 20 символов.";
                return false;
            }

            return true;
        }
    }
}
