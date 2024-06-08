using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class MaxImageMovieSizeAttribute : ValidationAttribute
    {
        private int _maxImageWidth;
        private int _maxImageHeight;

        public MaxImageMovieSizeAttribute(int maxImageWidth, int maxImageHeight)
        {
            _maxImageWidth = maxImageWidth;
            _maxImageHeight = maxImageHeight;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not IFormFile)

            {
                throw new ArgumentException($"You can use {nameof(MaxImageSizeAttribute)} only with IFormFile ");
            }

            var formFile = (IFormFile)value;

            using var stream = formFile.OpenReadStream();
            var image = Image.FromStream(stream);

            if (image.Width > _maxImageWidth || image.Height > _maxImageHeight)
            {
                return new ValidationResult(
                    $"The maximum width {_maxImageWidth}. " +
                    $"Maximum height {_maxImageHeight}");
            }

            return ValidationResult.Success;
        }
    }
}
