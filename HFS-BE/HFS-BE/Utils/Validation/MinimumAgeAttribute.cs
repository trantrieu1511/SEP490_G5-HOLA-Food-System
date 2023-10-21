using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.Validation
{
	public class MinimumAgeAttribute : ValidationAttribute
	{
		private readonly int _minimumAge;

		public MinimumAgeAttribute(int minimumAge)
		{
			_minimumAge = minimumAge;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime birthdate)
			{
				var age = CalculateAge(birthdate);
				if (age < _minimumAge)
				{
					return new ValidationResult($"You must be at least {_minimumAge} years old.");
				}
			}

			return ValidationResult.Success;
		}

		private int CalculateAge(DateTime birthdate)
		{
			var today = DateTime.Today;
			var age = today.Year - birthdate.Year;
			if (birthdate > today.AddYears(-age))
			{
				age--;
			}
			return age;
		}
	}
}
