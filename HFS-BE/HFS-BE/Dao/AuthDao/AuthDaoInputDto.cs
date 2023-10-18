using HFS_BE.Base;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Dao.AuthDao
{
    public class AuthDaoInputDto : BaseInputDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
