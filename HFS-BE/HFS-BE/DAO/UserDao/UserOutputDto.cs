using HFS_BE.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public bool IsOnline { get; set; }
        public bool confirmedEmail { get; set; }
        public decimal? WalletBalance { get; set; }
        public string? ManageBy { get; set; }
        public bool isBanned { get; set; }

		public bool? IsPhoneVerified { get; set; }
		public bool isVerified { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
    }

    public class UserProfileOutputDto : BaseOutputDto
    {
        public UserProfile? data { get; set;}
    }

    public class GetOrderInfoOutputDto : BaseOutputDto
    {
        public string? UserId { get; set; }
        public decimal? Balance { get; set; }
        public string? Address { get; set; }
        public bool? ConfirmEmail { get; set; }
        public bool? isPhoneVerify { get; set; }
    }

    public class GetUserAddressDaoOutputDto : BaseOutputDto
    {
        public string Phone { get; set; }
        public decimal? Balance { get; set; } = 0;
        public List<UserAddressDaoOutputDto> ListAddress { get; set; }
    }

    public class UserAddressDaoOutputDto
    {
        public int AddressId { get; set; }
        public string AddressInfo { get; set; }
        public bool? IsDefaultAddress { get; set; }
    }

    public class UserRefreshToken
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
