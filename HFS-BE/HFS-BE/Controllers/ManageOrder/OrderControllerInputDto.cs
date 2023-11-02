using HFS_BE.Base;
using HFS_BE.Utils.CustomValidation;
using Mailjet.Client.Resources;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.ManageOrder
{
    public class OrderSellerByStatusInputDto
    {
        [Required(ErrorMessage = "Status is not empty")]
        public byte? Status { get; set; }
        [Required]
        [DateNotInFuture(ErrorMessage = "DateFrom cannot be in the future")]
        public DateTime? DateFrom { get; set; }
        [Required]
        [DateNotInFuture(ErrorMessage = "DateEnd cannot be in the future")]
        [DateEndAfterDateFrom(ErrorMessage = "DateEnd must be greater than or equal to DateFrom")]
        public DateTime? DateEnd { get; set; }
    }

    public class OrderCancelInputDto
    {
        [Required(ErrorMessage = "OrderId is not empty")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Status is not empty")]
        [OrderProgressStatus(ErrorMessage = "Invalid status")]
        public byte Status { get; set; }
        [Required(ErrorMessage = "Note is not emtpy")]
        public string? Note { get; set; }
    }

    public class OrderAcceptInputDto
    {
        [Required(ErrorMessage = "OrderId is not empty")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Status is not empty")]
        [OrderProgressStatus(ErrorMessage = "Invalid status")]
        public byte Status { get; set; }
    }

    public class OrderInternalInputDto
    {
        [Required(ErrorMessage = "OrderId is not empty")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Status is not empty")]
        [OrderProgressStatus(ErrorMessage = "Invalid status")]
        public byte Status { get; set; }
        [Required(ErrorMessage = "ShipperId is not empty")]
        public string? ShipperId { get; set; }
    }
}
