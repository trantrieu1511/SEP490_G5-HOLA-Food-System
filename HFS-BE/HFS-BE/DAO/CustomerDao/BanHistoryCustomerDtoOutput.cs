using HFS_BE.Base;

namespace HFS_BE.DAO.CustomerDao
{
	public class BanHistoryCustomerDtoOutput:BaseOutputDto
	{
		public int BanCustomerId { get; set; }
		public string SellerId { get; set; }
		public string? Reason { get; set; }
		public DateTime CreateDate { get; set; }
	}

	public class ListHistoryBanCustomer : BaseOutputDto
	{
		public List<BanHistoryCustomerDtoOutput> data { get; set; }
	}
}
