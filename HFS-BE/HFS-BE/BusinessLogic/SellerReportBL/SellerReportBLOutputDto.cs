using HFS_BE.Base;
using HFS_BE.BusinessLogic.ReplyFeedBack;

namespace HFS_BE.BusinessLogic.SellerReportBL
{
	public class SellerReportBLOutputDto : BaseOutputDto
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
		public List<SellerReportImageOutputDto>? ImagesBase64 { get; set; } = new List<SellerReportImageOutputDto>();
	}

	public class SellerReportImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}
	public class SellerReportBLByCustomerOutputDto : BaseOutputDto
	{
		public int SellerReportId { get; set; }
		public string ShopName { get; set; } = null!;
		public string ReportContent { get; set; } = null!;
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public byte Status { get; set; }
		public string? Note { get; set; }
		public List<SellerReportImageOutputDto>? ImagesBase64 { get; set; } = new List<SellerReportImageOutputDto>();
	}
	public class ListSellerReportOutputDtoBL : BaseOutputDto
	{
		public List<SellerReportBLOutputDto> data { get; set; }
	}

	public class ListSellerReportByCustomerOutputDtoBL : BaseOutputDto
	{
		public List<SellerReportBLByCustomerOutputDto> data { get; set; }
	}

}
