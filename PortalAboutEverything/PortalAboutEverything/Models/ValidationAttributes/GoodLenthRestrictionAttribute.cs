using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class GoodLenthRestriction : ValidationAttribute
    {
        private string _defaultErrorMessage;

        private int _maxLength;

        private int _minLength;

        public GoodLenthRestriction(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

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

            string strValue = value.ToString()!;
            int length = strValue.Length;

            if (length < _minLength || length > _maxLength)
            {
                _defaultErrorMessage = $"Название товара должно быть не менее {_minLength} и не более {_maxLength} символов.";
                return false;
            }

            return true;
        }
    }
}
