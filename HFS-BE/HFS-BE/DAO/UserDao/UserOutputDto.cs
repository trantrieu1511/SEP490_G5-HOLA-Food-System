using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    public class UserProfileOutputDto: BaseOutputDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public long? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public bool IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public int? ManageBy { get; set; }
    }
}
