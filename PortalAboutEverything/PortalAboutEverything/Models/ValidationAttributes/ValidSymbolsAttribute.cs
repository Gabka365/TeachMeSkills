using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalAboutEverything.Models.ValidationAttributes
{
	public class ValidSymbolsAttribute : ValidationAttribute
	{
		public override string FormatErrorMessage(string name)
		{
			var defaultErrorMessage = $"""Поле "{name}" Содержит некорректные символы""";

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
			var expression = new Regex(@"[!#@%*?$№<>]");
			var match = expression.Match(text);
			return !match.Success;
		}
	}
}
