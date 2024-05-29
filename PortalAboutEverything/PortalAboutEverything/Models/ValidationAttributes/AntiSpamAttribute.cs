using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class AntiSpamAttribute : ValidationAttribute
    {
        private readonly int _maxRepeats;
        private readonly int _maxLinks;

        public AntiSpamAttribute(int maxRepeats = 3,  int maxLinks = 3)
        {
            _maxRepeats = maxRepeats;
            _maxLinks = maxLinks;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var input = value.ToString();

            var words = input.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.GroupBy(w => w).Any(g => g.Count() > _maxRepeats))
            {
                return new ValidationResult($"Спам это плохо");
            }

            var linkCount = Regex.Matches(input, @"\b((http|https)://)?(www\.)?([a-zA-Z0-9-]+\.[a-zA-Z]{2,})([/?#][^\s]*)?\b").Count;
            if (linkCount > _maxLinks)
            {
                return new ValidationResult($"Спам это плохо");
            }

            return ValidationResult.Success;
        }
    }
}
