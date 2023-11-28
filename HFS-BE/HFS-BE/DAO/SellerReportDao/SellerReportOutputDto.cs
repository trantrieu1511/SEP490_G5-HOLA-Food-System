using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.SellerReportDao
{
	public class SellerReportOutputDto
	{
		public int SellerReportId { get; set; }
		public string SellerId { get; set; } = null!;
		public string SellerName { get; set; } = null!;
		public string ShopName { get; set; } = null!;
		public string ReportBy { get; set; } = null!;
		public string ReportByName { get; set; } = null!;
		public string ReportContent { get; set; } = null!;
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public byte Status { get; set; }
		public string? UpdateBy { get; set; }
		public string? Note { get; set; }
		public List<SellerReportImage> Images { get; set; }
	}

	public class ListSellerReportOutputDto : BaseOutputDto
	{
		public List<SellerReportOutputDto> data { get; set; }
	}
	
}
