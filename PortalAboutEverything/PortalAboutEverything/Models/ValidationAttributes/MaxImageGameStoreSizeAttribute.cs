using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class MaxImageGameStoreSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not IFormFile)
            {
                throw new ArgumentException($"you ca use {nameof(MaxImageGameStoreSizeAttribute)} only with IFormFile ");

            }
            return base.IsValid(value, validationContext);
        }
    }
}
