using HFS_BE.DAO.ProfileImage;

namespace HFS_BE.DAO.CustomerDao
{
	public class CustomerDtoOutput
	{
		public string CustomerId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool? IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public bool? ConfirmEmail { get; set; }
		public bool? IsBanned { get; set; }
		 public List<ImageCustomerOutputDto>? Images { get; set; }
	}
	public class ImageCustomerOutputDto
	{
		public int ImageId { get; set; }
		public string UserId { get; set; } = null!;
		public string Path { get; set; } = null!;
		public bool IsReplaced { get; set; }
	}

	public class CustomerMessageDtoOutput
	{
		public string CustomerId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public long? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool? IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public bool ConfirmEmail { get; set; }
		public bool? IsBanned { get; set; }
		public int? CountMessageNotIsRead { get; set; }
		public string? Image { get; set; }
	}
}
