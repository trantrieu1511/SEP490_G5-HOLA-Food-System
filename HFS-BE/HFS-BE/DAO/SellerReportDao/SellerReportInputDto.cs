namespace HFS_BE.DAO.SellerReportDao
{
	public class SellerReportInputDto
	{
		public int SellerReportId { get; set; }
		public string SellerId { get; set; } = null!;
		public string ReportBy { get; set; } = null!;
		public string ReportContent { get; set; } = null!;
		public DateTime CreateDate { get; set; }
		public byte Status { get; set; }
		public List<string>? Images { get; set; }
	}
	public class SellerReportByAdminInputDto
	{
		public int SellerReportId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string? UpdateBy { get; set; }
		public byte Status { get; set; }
		public string? Note { get; set; }
	}

	public class SellerReportByCustomerInputDto
	{
	
		public string ReportBy { get; set; } = null!;
	

	}
}
