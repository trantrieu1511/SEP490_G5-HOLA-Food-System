using HFS_BE.Base;

namespace HFS_BE.DAO.SellerDao
{
	public class ActiveSellerDtoInput:BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public string Note { get; set; } = null!;
		public byte Status { get; set; }
	}
	public class RejectSellerDtoInput : BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public string Note { get; set; } = null!;
		public byte Status { get; set; }
	}
}
