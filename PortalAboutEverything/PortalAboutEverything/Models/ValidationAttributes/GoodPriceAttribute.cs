using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
    public class GoodPriceAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string name)
        {
            var defaultErrorMessage = $"""Поле "{name}" не может быть меньше либо равно нулю.""";

            return string.IsNullOrEmpty(ErrorMessage) ? defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                ErrorMessage = "Это поле должно быть заполнено.";
                return false;
            }

            if (value is not int)
            {
                throw new ArgumentException($"Неправильный тип валидации. {nameof(GoodPriceAttribute)} может работать только с числом");
            }


            var price = (int)value;

            return price > 0;
        }
    }
}
