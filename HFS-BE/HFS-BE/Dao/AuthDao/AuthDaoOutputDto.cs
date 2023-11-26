using HFS_BE.Base;

namespace HFS_BE.Dao.AuthDao
{
    public class AuthDaoOutputDto : BaseOutputDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
    }
}
