using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodOutputDto : BaseOutputDto
    {
        public int FoodId { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<FoodImageDto> foodImages { get; set; }
        public bool? Status { get; set; }
    }

    public class FoodImageDto
    {
        public int ImageId { get; set; }
        public string Path { get; set; }
    }

    public class FoodShopDaoOutputDto : BaseOutputDto
    {
        public List<FoodOutputDto> ListFood { get; set; }
    }


    public class FoodOutputSellerDto : BaseOutputDto
    {
        public int FoodId { get; set; }
        public string? SellerId { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<FoodImage> Images { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
        public string? Status { get; set; }
    }

    public class ListFoodOutputSellerDto : BaseOutputDto
    {
        public List<FoodOutputSellerDto> Foods { get; set; }
    }

    public class AddFoodOutput : BaseOutputDto
    {
        public int FoodId { get; set; }
    }
}
