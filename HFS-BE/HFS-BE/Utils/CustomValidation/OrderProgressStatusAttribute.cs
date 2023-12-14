using HFS_BE.Utils.Enum;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Utils.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class OrderProgressStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var statusStr = OrderProgressStatusEnum.GetOrderProgressStatusString((byte)value);
            if (!statusStr.Equals("unknown"))
            {
                return true;
            }
            return false;
        }
    }
}
