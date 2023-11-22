using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.Validation
{
	public class MinimumAgeAttribute : ValidationAttribute
	{
		private readonly int _minimumAge;
		private readonly int _maximumAge;

		public MinimumAgeAttribute(int minimumAge, int maximumAge)
		{
			_minimumAge = minimumAge;
			_maximumAge = maximumAge;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime birthdate)
			{
				var age = CalculateAge(birthdate);
				if (age < _minimumAge || age > _maximumAge)
				{
					return new ValidationResult($"Age must be between {_minimumAge} and {_maximumAge} years.");
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
