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
}
