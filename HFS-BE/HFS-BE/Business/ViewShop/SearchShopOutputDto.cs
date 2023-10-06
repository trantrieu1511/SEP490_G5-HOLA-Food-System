namespace HFS_BE.Business.ViewShop
{
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
