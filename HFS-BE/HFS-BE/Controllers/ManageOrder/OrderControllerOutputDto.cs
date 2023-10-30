using HFS_BE.Utils.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.ManageOrder
{
    public class OrderSellerByStatusInputDto
    {
        [Required]
        public byte? Status { get; set; }
        [Required]
        [DateNotInFuture(ErrorMessage = "DateFrom cannot be in the future")]
        public DateTime? DateFrom { get; set; }
        [Required]
        [DateNotInFuture(ErrorMessage = "DateEnd cannot be in the future")]
        public DateTime? DateEnd { get; set; }
    }
}
