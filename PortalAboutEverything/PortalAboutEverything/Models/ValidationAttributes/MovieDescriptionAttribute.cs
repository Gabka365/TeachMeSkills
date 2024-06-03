using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.ValidationAttributes
{
	public class MovieDescriptionAttribute : ValidationAttribute
	{
		private const int MAX_SYMBOLS = 100;

		public override string FormatErrorMessage(string name)
		{
			var defaultErrorMessage = $"""Описание фильма не должно быть больше {MAX_SYMBOLS} знаков""";

			return string.IsNullOrEmpty(ErrorMessage)
				? defaultErrorMessage
				: ErrorMessage;
		}

		public override bool IsValid(object? value)
		{
			if (value is null)
			{
				return true;
			}

			var text = (string)value;
			return text.Length <= MAX_SYMBOLS;
		}
	}
}
