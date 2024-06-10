using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions;

        public FileExtensionAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_validExtensions.Contains(extension))
                {
                    return new ValidationResult($"Допустимые расширения: {string.Join(", ", _validExtensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
