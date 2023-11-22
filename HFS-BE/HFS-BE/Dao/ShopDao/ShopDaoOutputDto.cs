using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.ShopDao
{
    public class ShopDto
    {
        public string UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Avatar { get; set; }
        public decimal? Star { get; set; }
        public int NumberOrdered { get; set; }
        public List<string> FoodImages { get; set; } = new List<string>();
    }

    public class DisplayShopDaoOutputDto : BaseOutputDto
    {
        public List<ShopDto> ListShop { get; set; }
    }

    public class GetShopDetailDaoOutputDto : BaseOutputDto
    {
        public string ShopId { get; set; }
        public long? PhoneNumber { get; set; }  
        public string? Avatar { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public bool IsOnline { get; set; }
    }
}
