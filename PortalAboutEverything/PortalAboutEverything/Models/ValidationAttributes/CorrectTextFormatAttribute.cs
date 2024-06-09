using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class CorrectTextFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not string)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(CorrectTextFormatAttribute)} can work only with string");
            }

            var text = (string)value;

            return text.Length <= 20;
        }
    }
}
