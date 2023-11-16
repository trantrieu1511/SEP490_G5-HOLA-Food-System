using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    public class GetUserProfileInputDto
    {
        public int UserId { get; set; }
    }

    public class EditUserProfileInputDto
    {
        //public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
    }

    public class GetOrderInfoInputDto
    {
        public string? UserId { get; set; }
    }

    public class GetAddressInfoInputDto
    {
        public string? UserId { get; set; }
    }
}
