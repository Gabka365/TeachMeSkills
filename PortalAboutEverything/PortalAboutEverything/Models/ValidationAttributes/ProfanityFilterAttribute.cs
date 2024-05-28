using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class ProfanityFilterAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> Profanities = new HashSet<string>
        {
            "нехорошо"
        };
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var input = value.ToString().ToLower();

            if (Profanities.Any(profanity => input.Contains(profanity)))
            {
                return new ValidationResult("Описание содержит нецензурные слова.");
            }

            return ValidationResult.Success;
        }
    }
}
