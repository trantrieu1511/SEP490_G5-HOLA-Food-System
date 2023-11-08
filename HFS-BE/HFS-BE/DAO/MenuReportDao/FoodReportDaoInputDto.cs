namespace HFS_BE.DAO.MenuReportDao
{
    public class CreateNewFoodReportInputDto
    {
        public int FoodId { get; set; }
        public string ReportContent { get; set; } = string.Empty;
    }

    public class ApproveNotApproveFoodReportInputDto
    {
        public int FoodId { get; set; }
        public string ReportBy { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
    }
}
