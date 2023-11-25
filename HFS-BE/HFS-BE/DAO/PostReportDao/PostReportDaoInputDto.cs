namespace HFS_BE.DAO.PostReportDao
{
    public class CreateNewPostReportInputDto
    {
        public int PostId { get; set; }
        //public string ReportBy { get; set; }
        public string ReportContent { get; set; }
        //public DateTime CreateDate { get; set; }
    }

    public class ApproveNotApprovePostReportInputDto
    {
        public int PostId { get; set; }
        public string ReportBy { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
    }
    
    public class CancelPostReportInputDto
    {
        public int PostId { get; set; }
        //public bool IsCancelled { get; set; }
        public string? Note { get; set; } // Viet ly do cancel to cao
    }

    public class DashboardPostModInputDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateEnd { get; set; }

        public string? ModId { get; set; }
    }
}
