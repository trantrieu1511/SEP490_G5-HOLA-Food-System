using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageFood
{


    public class FoodOutputSellerDto
    {
        public int FoodId { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int Rating { get; set; }
        public string? Status { get; set; }
        public List<FoodImageOutputSellerDto>? ImagesBase64 { get; set; } = new List<FoodImageOutputSellerDto>();
    }

    public class FoodImageOutputSellerDto
    {
        public int ImageId { get; set; }
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
    }

    public class ListFoodOutputSellerDto : BaseOutputDto
    {
        public List<FoodOutputSellerDto> Foods { get; set; }
    }
}
