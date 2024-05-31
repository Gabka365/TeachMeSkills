using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class TextInputAttribute : ValidationAttribute
    {
        private readonly int _minLenght;
        private readonly int _maxLenght;
        private string _errorMessage;

        public TextInputAttribute(int minLenght, int maxLenght)
        {
            _minLenght = minLenght;
            _maxLenght = maxLenght;
        }

        public override string FormatErrorMessage(string name)
        {
            string errorMessageWithFormat = string.Empty;
            if (_errorMessage is not null)
            {
                errorMessageWithFormat = string.Format(_errorMessage, name);
            }

            return string.IsNullOrEmpty(ErrorMessage) ? errorMessageWithFormat : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            string valueInString = (string)value;

            if (valueInString.Length < _minLenght)
            {
                _errorMessage = "Значение поля \"{0}\" не может быть короче " + _minLenght + " " + GetlWithCorrectEnding(_minLenght);
                return false;
            }

            if (valueInString.Length > _maxLenght)
            {
                _errorMessage = "Значение поля \"{0}\" не может быть длиннее " + _maxLenght + " " + GetlWithCorrectEnding(_maxLenght);
                return false;
            }

            return true;
        }

        private string GetlWithCorrectEnding(int number)
        {
            int lastDigit = number % 10;
            int lastTwoDigit = number % 100;

            if (lastDigit == 1 && lastTwoDigit != 11)
            { 
                return "символа"; 
            }
            else
            { 
                return "символов"; 
            }
        }
    }
}
