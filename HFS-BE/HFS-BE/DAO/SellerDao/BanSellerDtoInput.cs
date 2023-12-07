using HFS_BE.Base;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.SellerDao
{
	public class BanSellerDtoInput : BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		[Required(ErrorMessage = "Reason is required")]
		public string? Reason { get; set; }
		public bool? IsBanned { get; set; }
	}
	public class BanSellerHistoryDtoInput : BaseInputDto
	{
		public string SellerId { get; set; }
	}
}
