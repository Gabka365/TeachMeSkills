using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class GoodDescriptionAttribute : ValidationAttribute
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
                _defaultErrorMessage = "Введите описание товара.";
                return false;
            }

            string strValue = value.ToString()!;
            int length = strValue.Length;

            if (length < 10 || length > 30)
            {
                _defaultErrorMessage = "Описание товара должно быть не менее 10 и не боее 30 символов.";
                return false;
            }

            return true;
        }
    }
}

