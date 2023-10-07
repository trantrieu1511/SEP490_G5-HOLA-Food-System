namespace HFS_BE.Business.ViewShop
{
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
    public class SearchShopOututDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }
}
