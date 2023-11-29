using HFS_BE.Base;
using HFS_BE.Utils.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.Dashboard
{
    public class DashboardDataSets
    {
        public string? Label { get; set; }
        public ICollection<dynamic>? Data { get; set; } = new List<dynamic>();
        public bool Fill { get; set; }
        public float Tension { get; set; }
    }

    public class DashboardSellerOutput : BaseOutputDto
    {
        public ICollection<string>? Labels { get; set; }
        public ICollection<DashboardDataSets>? Datasets { get; set; }
        public int OrderCount { get; set; }
        public decimal OrderCountPercent { get; set; }
        public decimal? AmountCount { get; set; }
        public decimal AmountCountPercent { get; set; }
        public int SoldCount { get; set; }
        public decimal SoldCountPercent { get; set; }

    }

    public class DashboardPostModStatistic
    {
        public int? TotalPosts { get; set; } // total posts in the system
        public int? TotalBannedPosts { get; set; } // total posts in the system which has been banned
        public int? TotalPostReports { get; set; } // total post reports in the system
        public int? TotalPendingPostReports { get; set; } // total pending post reports in the system
        public int? TotalApprovedPostReports { get; set; } // total approved posts of the post mod in the system
        public int? TotalNotapprovedPostReports { get; set; } // total not approved posts of the post mod in the system
    }

    public class DashboardPostModStatisticOutput : BaseOutputDto
    {
        public DashboardPostModStatistic Statistics { get; set; } = new DashboardPostModStatistic(); // So lieu thong ke cua ca he thong
        public DashboardPostModStatistic MyStatistics { get; set; } = new DashboardPostModStatistic(); // So lieu thong ke cua 1 post mod
    }

    public class DashboardPostmodOutput : BaseOutputDto
    {
        public ICollection<string>? Labels { get; set; }
        public ICollection<DashboardDataSets>? Datasets { get; set; }
        public int? TotalPosts { get; set; } = 0;
        public int? TotalBannedPosts { get; set; } = 0;
        public int? TotalPostReports { get; set; } = 0;
        public int? TotalPendingPostReports { get; set; } = 0;
        public int? TotalApprovedPostReports { get; set; } = 0;
        public int? TotalNotApprovedPostReports { get; set; } = 0;
    }

    public class DashboardMenuModStatistic
    {
        public int? TotalFood { get; set; } // total food in the system
        public int? TotalBannedFood { get; set; } // total food in the system which has been banned
        public int? TotalFoodReports { get; set; } // total food reports in the system
        public int? TotalPendingFoodReports { get; set; } // total pending food reports in the system
        public int? TotalApprovedFoodReports { get; set; } // total approved food of the menu mod in the system
        public int? TotalNotapprovedFoodReports { get; set; } // total not approved food of the menu mod in the system
    }

    public class DashboardMenuModStatisticOutput : BaseOutputDto
    {
        public DashboardMenuModStatistic Statistics { get; set; } = new DashboardMenuModStatistic(); // So lieu thong ke cua ca he thong
        public DashboardMenuModStatistic MyStatistics { get; set; } = new DashboardMenuModStatistic(); // So lieu thong ke cua 1 menu mod
    }

    public class DashboardMenumodOutput : BaseOutputDto
    {
        public ICollection<string>? Labels { get; set; }
        public ICollection<DashboardDataSets>? Datasets { get; set; }
        public int? TotalFood { get; set; } = 0;
        public int? TotalBannedFood { get; set; } = 0;
        public int? TotalFoodReports { get; set; } = 0;
        public int? TotalPendingFoodReports { get; set; } = 0;
        public int? TotalApprovedFoodReports { get; set; } = 0;
        public int? TotalNotApprovedFoodReports { get; set; } = 0;
    }
}
