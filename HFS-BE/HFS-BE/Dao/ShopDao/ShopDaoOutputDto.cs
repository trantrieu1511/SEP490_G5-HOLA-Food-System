using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.ShopDao
{
    public class UserSearchDto : BaseOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchShopOututDto : BaseOutputDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }

    public class ShopDto
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Avatar { get; set; }
    }

    public class DisplayShopOutputDto : BaseOutputDto
    {
        public List<ShopDto> ListShop { get; set; }
    }
}
