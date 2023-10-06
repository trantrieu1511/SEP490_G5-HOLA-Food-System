using HFS_BE.Models;

namespace HFS_BE.DAO.UserDAO
{
    public class UserSearchDto
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

    public class SearchShopOututDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }
}
