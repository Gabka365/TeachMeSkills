using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class GoodNoSpecialCharactersAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            string stringValue = value.ToString()!;

            Regex regex = new Regex("^[a-zA-Z0-9 ]+$");

            if (!regex.IsMatch(stringValue))
            {
                ErrorMessage = "Это поле не может содержать специальные символы.";
                return false;
            }

            return true;
        }
    }
}
