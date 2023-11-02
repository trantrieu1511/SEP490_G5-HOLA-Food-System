using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    /// <summary>
    /// Profile info of all users (admin/seller/customer/shipper/post moderator/menu moderator), used by UserProfileOutputDto for display
    /// </summary>
    public class UserProfile
    {
        public string? UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public long? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public bool IsOnline { get; set; }
        public bool confirmedEmail { get; set; }
        public decimal? WalletBalance { get; set; }
        public int? ManageBy { get; set; }
        public bool isBanned { get; set; }
        public bool isVerified { get; set; }
    }

    public class UserProfileOutputDto : BaseOutputDto
    {
        public UserProfile? data { get; set;}
    }

    public class GetOrderInfoOutputDto : BaseOutputDto
    {
        public decimal? Balance { get; set; }
        public string? Address { get; set; }
    }

}
