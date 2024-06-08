using PortalAboutEverything.LocalizationResources;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class MovieDescriptionAttribute : ValidationAttribute
    {
        private const int MAX_SYMBOLS = 100;

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessageTemplate = Movie_CreateMovie.MovieDescription_ValidationErrorMessage;

            if (ErrorMessageResourceType is not null
                && ErrorMessageResourceName is not null)
            {
                var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceName);
                var value = property!.GetValue(null);
                defaultErrorMessageTemplate = (string)value!;
            }

            var defaultErrorMessage = string.Format(defaultErrorMessageTemplate, MAX_SYMBOLS);

            return string.IsNullOrEmpty(ErrorMessage)
                ? defaultErrorMessage
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            var text = (string)value;
            return text.Length <= MAX_SYMBOLS;
        }
    }
}
