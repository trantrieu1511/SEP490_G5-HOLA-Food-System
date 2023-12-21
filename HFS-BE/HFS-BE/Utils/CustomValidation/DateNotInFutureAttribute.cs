using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                //Console.WriteLine("dateFrom: " + dateTime);
                //Console.WriteLine("dateNow: " + DateTime.Now);
                //Console.WriteLine("compare: " + dateTime.Date.CompareTo(DateTime.Now.Date));
                return dateTime.Date.CompareTo(DateTime.Now.Date) <= 0 ? true : false;
            }
            return false;
        }
    }
}
