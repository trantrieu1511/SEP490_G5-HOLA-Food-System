using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HFS_BE.Utils.Validation
{
	public class PasswordAttribute : ValidationAttribute
	{
		private readonly int _minimumLength;

		public PasswordAttribute(int minimumLength)
		{
			_minimumLength = minimumLength;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string password = value.ToString();

				if (password.Length < _minimumLength)
				{
					return new ValidationResult($"Password must be at least {_minimumLength} characters long.");
				}

			
				if (!Regex.IsMatch(password, @"\d+"))
				{
					return new ValidationResult("Password must contain at least one digit.");
				}

				
				if (!Regex.IsMatch(password, @"[A-Z]+"))
				{
					return new ValidationResult("Password must contain at least one uppercase letter.");
				}
			}

			return ValidationResult.Success;
		}
	}
}
