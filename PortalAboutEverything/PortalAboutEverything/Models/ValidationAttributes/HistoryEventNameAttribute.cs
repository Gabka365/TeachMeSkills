using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class HistoryEventNameAttribute : ValidationAttribute
    {
        private string _defaultErrorMessage;
                
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? _defaultErrorMessage
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }
                
            var Name = (string)value;
            if (int.TryParse(Name, out int numbers) == true)
            {
                _defaultErrorMessage = "Название не может состоять только из цифр";
                return false;
            }
            return true;
        }

    }
}
