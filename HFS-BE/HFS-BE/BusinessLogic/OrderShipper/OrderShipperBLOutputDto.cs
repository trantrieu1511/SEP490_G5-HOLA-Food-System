using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderBLOutputDto
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

        public List<OrderDetailBLDto> OrderDetails { get; set; }

        public List<OrderProgressDto>? OrderProgress { get; set; }

    }

    public class OrderByShipperBLOutputDto : BaseOutputDto
    {
        public List<OrderBLOutputDto> Orders { get; set; }
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
    public class OrderProgressDto
    {
        public int OrderProgressId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public ImageFoodOutputDto? ImageBase64 { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class ImageFoodOutputDto
    {
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }

    }

}
