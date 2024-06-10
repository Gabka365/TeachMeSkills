using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class HistoryEventYearAttribute : ValidationAttribute
    {
        private const int MinYear = 1300;
        private const int MaxYear = 1500;
        
        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                throw new ArgumentException($"Wrong type for validation. {nameof(HistoryEventYearAttribute)} can work only with int");
            }

            var year = (int)value;

            return year > MinYear && year <= MaxYear;
        }
    }
}
