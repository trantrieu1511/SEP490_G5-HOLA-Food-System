using HFS_BE.Base;

namespace HFS_BE.DAO.PostReportDao
{
    /// <summary>
    /// Post report output data transfer object of post moderator, we used this object to display output on the response body of the API
    /// </summary>
    public class PostReportOutputDto
    {
        public int PostId { get; set; }
        public string? SellerName { get; set; }
        public string? ShopName { get; set; }
        public string? PostContent { get; set; }
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = null;
        public string? UpdateBy { get; set; } = null;
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
    }

    /// <summary>
    /// A class which contains a list of post report output dto
    /// </summary>
    public class ListPostReportOutputDto : BaseOutputDto
    {
        public List<PostReportOutputDto> PostReports { get; set; } = new List<PostReportOutputDto>();
    }
}
