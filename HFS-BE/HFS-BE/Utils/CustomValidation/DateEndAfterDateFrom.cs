using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.CustomValidation
{
    public class DateEndAfterDateFrom : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"The {name} must be greater than or equal to the DateFrom.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateFromProperty = validationContext.ObjectType.GetProperty("DateFrom");
            var dateFromValue = (DateTime?)dateFromProperty.GetValue(validationContext.ObjectInstance, null);

            var dateEndValue = (DateTime?)value;

            if (dateFromValue.HasValue && dateEndValue.HasValue && dateEndValue < dateFromValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
