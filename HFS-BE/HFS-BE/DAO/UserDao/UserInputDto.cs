using HFS_BE.Base;

namespace HFS_BE.DAO.UserDao
{
    public class GetUserProfileInputDto
    {
        public int UserId { get; set; }
    }

    public class EditCardInputDto
    {
		public string Email { get; set; }
		public string IdcardNumber { get; set; }
		public IFormFile? Images1 { get; set; } = null;
		public IFormFile? Images2 { get; set; } = null;

	}
	public class EditUserProfileInputDto
	{
		//public string UserId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? ShopName { get; set; }
		public string? ShopAddress { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public bool IsNewPhonenumber { get; set; }
	}
	public class VerifyIdentityInputDto
    {
        public string Password { get; set; } = string.Empty; // Password nhap boi nguoi dung de xac nhan danh tinh
        //public byte[] PasswordSalt { get; set; } = null!;
        //public byte[] PasswordHash { get; set; } = null!;
    }
    
    public class ChangeUserAccountPasswordInputDto
    {
        public string NewPassword { get; set; } = string.Empty; // Password moi cua nguoi dung
        public string ConfirmPassword { get; set; } = string.Empty; // Xac nhan pass word moi bang cach nhap lai no
    }

    public class GetOrderInfoInputDto
    {
        public string? UserId { get; set; }
    }
	public class ShipperEditInputDto
	{
		public string? UserId { get; set; }
		public string IdcardNumber { get; set; }
		public string? IdcardBackImage { get; set; } = null;
		public string? IdcardFrontImage { get; set; } = null;
	}
	public class GetAddressInfoInputDto
    {
        public string? UserId { get; set; }
    }

    public class UpdateRefreshToken
    {
        public string? UserId { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class AddRefreshToken
    {
        public string? UserId { get; set; }
        public string? Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
