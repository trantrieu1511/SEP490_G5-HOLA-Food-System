using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderShipperBLOutputDto
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? SellerId { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public string PaymentMethod { get; set; }
        public string? Status { get; set; }

        public List<OrderDetailBLDto>? OrderDetails { get; set; }

        public List<OrderProgressBLDto>? OrderProgresses { get; set; }

    }

    public class OrderByShipperBLOutputDto : BaseOutputDto
    {
        public List<OrderShipperBLOutputDto> Orders { get; set; }
    }

    public class OrderDetailBLDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string SellerId { get; set; }
        public string FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public ImageFoodOutputDto ImageBase64 { get; set; }
    }
    public class OrderProgressBLDto
    {
        public ImageFoodOutputDto? ImageBase64 { get; set; }
        public string? Note { get; set; }
        public string? CreateDate { get; set; }
        public string? Status { get; set; }
    }

    public class ImageFoodOutputDto
    {
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }

    }

}
