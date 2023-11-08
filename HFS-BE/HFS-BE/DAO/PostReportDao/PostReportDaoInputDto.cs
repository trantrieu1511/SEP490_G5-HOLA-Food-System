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
}
