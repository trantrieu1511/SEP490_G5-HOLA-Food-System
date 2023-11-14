using HFS_BE.Base;

namespace HFS_BE.DAO.SellerDao
{
	public class ListSellerDtoOutput:BaseOutputDto
	{
	public List<SellerDtoOutput> data {  get; set; }
	}
	public class BanHistorySellerDtoOutput : BaseOutputDto
	{
		public int BanSellerId { get; set; }
		public string SellerId { get; set; }
		public string? Reason { get; set; }
		public DateTime CreateDate { get; set; }
	}

	public class ListHistoryBanSeller : BaseOutputDto
	{
		public List<BanHistorySellerDtoOutput> data { get; set; }
	}
}
