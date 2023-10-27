using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    public class GetUserProfileInputDto
    {
        public int UserId { get; set; }
    }

    public class EditUserProfileInputDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class GetOrderInfoInputDto
    {
        public int? UserId { get; set; }
    }
}
