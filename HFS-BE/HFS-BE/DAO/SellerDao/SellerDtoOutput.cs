using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.SellerDao
{
	public class SellerDtoOutput:BaseOutputDto
	{
		public string SellerId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public string? ShopName { get; set; }
		public string? ShopAddress { get; set; }
		public bool? ConfirmedEmail { get; set; }
		public bool? IsBanned { get; set; }
		public bool? IsVerified { get; set; }
		public string? BusinessCode { get; set; }
		public List<ImageSellerOutputDto>? Images { get; set; }
		public List<SellerLicenseImage>? ImagesL { get; set; }

	}
	public class ImageSellerLOutputDto
	{
		public int ImageId { get; set; }
		public string UserId { get; set; } = null!;
		public string Path { get; set; } = null!;
	}
	public class ImageSellerOutputDto
	{
		public int ImageId { get; set; }
		public string UserId { get; set; } = null!;
		public string Path { get; set; } = null!;
		public bool IsReplaced { get; set; }
	}
	public class SellerMessageDtoOutput : BaseOutputDto
	{
		public string SellerId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public long? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public string? ShopName { get; set; }
		public string? ShopAddress { get; set; }
		public bool? ConfirmedEmail { get; set; }
		public bool? IsBanned { get; set; }
		public bool? IsVerified { get; set; }
		public int? CountMessageNotIsRead { get; set; }
		public string? Image { get; set; }
	}
}
