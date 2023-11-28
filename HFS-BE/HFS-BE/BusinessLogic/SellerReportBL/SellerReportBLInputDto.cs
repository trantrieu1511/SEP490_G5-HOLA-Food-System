using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.SellerReportBL
{
	public class SellerReportBLInputDto
	{

	
	public string SellerId { get; set; } = null!;
	public string? ReportBy { get; set; } = null!;
	public string ReportContent { get; set; } = null!;

	public IReadOnlyList<IFormFile>? Images { get; set; } = null;

	 //public UserDto UserDto { get; set; }
		
	}
}
