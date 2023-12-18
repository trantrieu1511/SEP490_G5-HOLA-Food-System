using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateNotInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }
}
