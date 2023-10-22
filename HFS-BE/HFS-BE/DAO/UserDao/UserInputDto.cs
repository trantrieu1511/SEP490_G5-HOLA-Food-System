using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    public class GetUserProfileInputDto: BaseInputDto
    {
        public int UserId { get; set; }
    }

    public class GetOrderInfoInputDto
    {
        public int? UserId { get; set; }
    }
}
