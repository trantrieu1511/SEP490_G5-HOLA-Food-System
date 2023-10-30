using HFS_BE.Base;

namespace HFS_BE.DAO.SellerDao
{
	public class ActiveSellerDtoInput:BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public bool? CheckSeller { get; set; }
	}
}
