using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.UserDAO
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
    public class UserDto : BaseOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchShopOututDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }
}
