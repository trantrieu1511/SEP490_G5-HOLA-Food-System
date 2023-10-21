using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class RequiredIfNotNullAttribute : ValidationAttribute
    {
        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }*/

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return name;
        }
    }
}
