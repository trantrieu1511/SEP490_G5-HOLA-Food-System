using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.ManageUser.ManageCustomer
{
	public class CustomerDtoBS
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
		public bool? ConfirmEmail { get; set; }
		public bool? IsBanned { get; set; }
		public List<CustomerImageOutputDto>? ImagesBase64 { get; set; } = new List<CustomerImageOutputDto>();
	}

	public class CustomerImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}

	public class ListCustomerOutputDtoBS : BaseOutputDto
	{
		public List<CustomerDtoBS> Customers { get; set; }
	}
}
