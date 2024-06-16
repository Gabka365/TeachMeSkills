using PortalAboutEverything.LocalizationResources;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ForbiddenSymbolsAttribute : ValidationAttribute
    {
        private string _forbiddenSymbols;

        public ForbiddenSymbolsAttribute(string forbiddenSymbols)
        {
            _forbiddenSymbols = forbiddenSymbols;
        }

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessageTemplate = Movie_CreateMovie.ForbiddenSymbols_ValidationErrorMessage;

            if (ErrorMessageResourceType is not null
                && ErrorMessageResourceName is not null)
            {
                var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceName);
                var value = property!.GetValue(null);
                defaultErrorMessageTemplate = (string)value!;
            }

            var defaultErrorMessage = string.Format(defaultErrorMessageTemplate, name);

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
            var expression = new Regex($"[{_forbiddenSymbols}]");
            var match = expression.Match(text);
            return !match.Success;
        }
    }
}
