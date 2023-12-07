using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HFS_BE.Utils.Validation
{
	public class PhoneNumberAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string phoneNumber = value.ToString();

				if (phoneNumber.Length == 10)
				{
				
					if (Regex.IsMatch(phoneNumber, "^(09|08|03|05)"))
					{
						return ValidationResult.Success;
					}
				}
			}

			return new ValidationResult("Invalid phone number format.");
		}
	}
}
