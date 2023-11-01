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
		public long? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool? IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public bool ConfirmEmail { get; set; }
		public bool? IsBanned { get; set; }
	}
}
