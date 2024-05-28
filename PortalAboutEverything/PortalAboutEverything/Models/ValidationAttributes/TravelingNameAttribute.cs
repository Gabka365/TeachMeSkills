using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class TravelingNameAttribute : ValidationAttribute
    {
        private const int MAX_SIMBOL = 20;
    
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            var input = value.ToString();
            var regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ]+$");

            if (!regex.IsMatch(input))
            {
                return new ValidationResult("Название  должно содержать только буквы английского и русского алфавитов.");
            }

            
            if (input.Length > MAX_SIMBOL)
            {
                return new ValidationResult("Название не должно быть длиннее 20 символов.");
            }

            return ValidationResult.Success;
        }
    }
}
