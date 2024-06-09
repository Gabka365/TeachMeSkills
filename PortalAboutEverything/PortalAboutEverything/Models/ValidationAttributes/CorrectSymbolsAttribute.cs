using PortalAboutEverything.Models.Auth;
using PortalAboutEverything.Models.Blog;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class CorrectSymbolsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as MessageViewModel;
            if (viewModel == null)
            {
                throw new Exception($"{nameof(CorrectSymbolsAttribute)} can be part only with {nameof(MessageViewModel)}");
            }

            if (viewModel.Number_1 == viewModel.Number_2)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(
            "Numbers are different",
            new List<string> { nameof(MessageViewModel.Number_1), nameof(MessageViewModel.Number_2) }
            );
        }
    }
}
