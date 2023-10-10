using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.ShopDao
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSearchDto : BaseOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchShopOututDto : BaseOutputDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopDto
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Avatar { get; set; }
    }

    /// <summary>
    /// Output of DisplayShop.
    /// </summary>
    public class DisplayShopOutputDto : BaseOutputDto
    {
        public List<ShopDto> ListShop { get; set; }
    }
}
