using HFS_BE.Utils.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.Dashboard
{
    public class DashboardInputDto
    {
        [Required]
        [DateNotInFuture(ErrorMessage = "DateFrom cannot be in the future")]
        public DateTime? DateFrom { get; set; }
        [Required]
        [DateNotInFuture(ErrorMessage = "DateEnd cannot be in the future")]
        [DateEndAfterDateFrom(ErrorMessage = "DateEnd must be greater than or equal to DateFrom")]
        public DateTime? DateEnd { get; set; }
    }
}
