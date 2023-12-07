using HFS_BE.Base;

namespace HFS_BE.DAO.CustomerDao
{
	public class BanCustomerDtoInput:BaseInputDto
	{
		public string CustomerId { get; set; } = null!;
		public string? Reason { get; set; }
		public bool? IsBanned { get; set; }
	}
	public class BanCustomerHistoryDtoInput : BaseInputDto
	{
		public string CustomerId { get; set; } 
	}
}
