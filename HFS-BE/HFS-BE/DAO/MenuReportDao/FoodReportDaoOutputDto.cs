using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.MenuReportDao
{
    /// <summary>
    /// Menu report output data transfer object of menu moderator, we used this object to display output on the response body of the API
    /// </summary>
    public class FoodReportOutputDto
    {
        public int FoodId { get; set; }
        public string? ShopName { get; set; }
        public string? FoodName { get; set; }
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = null;
        public string? UpdateBy { get; set; } = null;
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
    }

    /// <summary>
    /// A class which contains a list of food report output dto
    /// </summary>
    public class ListFoodReportOutputDto : BaseOutputDto
    {
        public List<FoodReportOutputDto> FoodReports { get; set; } = new List<FoodReportOutputDto>();
    }

    public class DashboardMenumodFoodReportDataDaoOutputDto : BaseOutputDto
    {
        public List<MenuReport> FoodReports { get; set; } = new List<MenuReport>();
    }
}
