using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Dashboard;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Dashboard.Menumoderator
{
    public class DashboardMenumodBusinessLogic : BaseBusinessLogic
    {
        public DashboardMenumodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DashboardMenuModStatisticOutput GetAllTimeStatistics(string userId)
        {
            try
            {
                var dao = CreateDao<FoodDao>();
                return dao.GetAllTimeStatistics(userId);
            }
            catch (Exception)
            {
                return Output<DashboardMenuModStatisticOutput>(Constants.ResultCdFail);
            }
        }

        /// <summary>
        /// Get the total of approved and not approve food reports of the menu moderator
        /// </summary>
        /// <param name="inputDto">Including DateFrom, DateEnd and ModId</param>
        /// <returns>DashboardMenumodOutput</returns>
        public DashboardMenumodOutput GetMyStatisticsFromDateRange(DAO.MenuReportDao.DashboardMenuModInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<FoodReportDao>();
                var foodReportList = dao.GetAllFoodReportsByModId(inputDto);
                if (!foodReportList.Success)
                {
                    return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
                }

                var output = Output<DashboardMenumodOutput>(Constants.ResultCdSuccess);

                /*
                 * 0. get total approve/not approve food report follow range Date input
                 * 1. datefrom == dateEnd -> ko can lay labels va dataSets
                    return 
                 * 2. datefrom != dateEnd -> get labels and dataSets
                */

                // 0. get count
                //set statistics

                output.TotalApprovedFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && fr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && fr.Status == 1)
                    .Count();
                output.TotalNotApprovedFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && fr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && fr.Status == 2)
                    .Count();

                // 1. datefrom == dateEnd
                if (inputDto.DateFrom.Equals(inputDto.DateEnd))
                {
                    return output;
                }

                // 2.datefrom != dateEnd -> get labels and dataSets

                output.Labels = CalculateLabels((DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);
                output.Datasets = CalculateMyDataSets(foodReportList.FoodReports, (DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);

                return output;
            }
            catch (Exception)
            {
                return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
            }
        }

        public DashboardMenumodOutput GetSystemStatisticsFromDateRange(DAO.MenuReportDao.DashboardMenuModInputDto inputDto)
        {
            try
            {
                var foodReportDao = CreateDao<FoodReportDao>();
                var foodDao = CreateDao<FoodDao>();

                var foodReportList = foodReportDao.GetAllFoodReports();
                if (!foodReportList.Success)
                {
                    return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
                }

                var foodList = foodDao.GetAllFood();
                if (!foodList.Success)
                {
                    return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
                }

                var output = Output<DashboardMenumodOutput>(Constants.ResultCdSuccess);

                /*
                 * 0. get system statistics follow range Date input
                 * 1. datefrom == dateEnd -> ko can lay labels va dataSets
                    return 
                 * 2. datefrom != dateEnd -> get labels and dataSets
                */

                // 0. get system statistics

                output.TotalFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.CreateDate.Date >= inputDto.DateFrom
                    && fr.CreateDate.Date <= inputDto.DateEnd)
                    .Count();
                output.TotalPendingFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.CreateDate.Date >= inputDto.DateFrom
                    && fr.CreateDate.Date <= inputDto.DateEnd
                    && fr.Status == 0)
                    .Count();
                output.TotalApprovedFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.CreateDate.Date >= inputDto.DateFrom
                    && fr.CreateDate.Date <= inputDto.DateEnd
                    && fr.Status == 1)
                    .Count();
                output.TotalNotApprovedFoodReports = foodReportList.FoodReports
                    .Where(fr => fr.CreateDate.Date >= inputDto.DateFrom
                    && fr.CreateDate.Date <= inputDto.DateEnd
                    && fr.Status == 2)
                    .Count();

                // 1. datefrom == dateEnd
                if (inputDto.DateFrom.Equals(inputDto.DateEnd))
                {
                    return output;
                }

                // 2.datefrom != dateEnd -> get labels and dataSets

                output.Labels = CalculateLabels((DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);
                output.Datasets = CalculateSystemDataSets(foodList.Foods, foodReportList.FoodReports, (DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);

                return output;
            }
            catch (Exception)
            {
                return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
            }
        }

        ICollection<string> CalculateLabels(DateTime dateFrom, DateTime dateEnd)
        {
            int numberOfDays = (dateEnd - dateFrom).Days + 1; // Số ngày trong khoảng thời gian

            List<string> dateList = new List<string>();

            DateTime currentDate = dateFrom; // Ngày hiện tại

            for (int i = 0; i < numberOfDays; i++)
            {
                if (currentDate.Date <= dateEnd.Date)
                {
                    dateList.Add(currentDate.Date.ToString("dd/MM/yyyy"));
                }
                else
                {
                    break;
                }

                currentDate = currentDate.AddDays(1);
            }

            return dateList;
        }

        /// <summary>
        /// Calculate to get the menu moderator's own statistics
        /// </summary>
        /// <param name="foodReports">List of food reports</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateEnd">Date to</param>
        /// <returns>ICollection<DashboardDataSets></returns>
        ICollection<DashboardDataSets> CalculateMyDataSets(List<MenuReport> foodReports, DateTime dateFrom, DateTime dateEnd)
        {
            List<DashboardDataSets> dataSets = new List<DashboardDataSets>();
            // get approved food report each date
            DashboardDataSets approvedFoodReportData = new DashboardDataSets
            {
                Label = "Approved food report"
            };
            // get not approved food report each date
            DashboardDataSets notApprovedFoodReportData = new DashboardDataSets
            {
                Label = "Not approved food report"
            };

            int numberOfDays = (dateEnd - dateFrom).Days + 1;

            DateTime currentDate = dateFrom; // Ngày hiện tại

            for (int i = 0; i < numberOfDays; i++)
            {
                if (currentDate.Date <= dateEnd.Date)
                {
                    // get list of current date food reports that are approved/not approved
                    var approvedNotApprovedFoodReportCurrentDate = foodReports.Where(fr => fr.UpdateDate.Value.Date == currentDate.Date).ToList();

                    // get amount of approved food report of the current date
                    var currentDateApprovedFoodReportAmount = approvedNotApprovedFoodReportCurrentDate.Where(fr => fr.Status == 1).Count();
                    approvedFoodReportData.Data.Add(currentDateApprovedFoodReportAmount);

                    // get amount of not approved food report of the current date
                    var currentDateNotApprovedFoodReportAmount = approvedNotApprovedFoodReportCurrentDate.Where(fr => fr.Status == 2).Count();
                    notApprovedFoodReportData.Data.Add(currentDateNotApprovedFoodReportAmount);
                }
                else
                {
                    break;
                }

                currentDate = currentDate.AddDays(1);
            }

            dataSets.Add(approvedFoodReportData);
            dataSets.Add(notApprovedFoodReportData);

            return dataSets;
        }

        /// <summary>
        /// Calculate to get the system statistics that relates to the menu moderator role
        /// </summary>
        /// <param name="foodReports">List of food reports</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateEnd">Date to</param>
        /// <returns>ICollection<DashboardDataSets></returns>
        ICollection<DashboardDataSets> CalculateSystemDataSets(List<Food> foods, List<MenuReport> foodReports, DateTime dateFrom, DateTime dateEnd)
        {
            List<DashboardDataSets> dataSets = new List<DashboardDataSets>();

            // get no of food each date
            //DashboardDataSets foodData = new DashboardDataSets
            //{
            //    Label = "Food"
            //};
            // get no of food reports each date
            DashboardDataSets foodReportData = new DashboardDataSets
            {
                Label = "Food report"
            };
            // get no of pending food report each date
            DashboardDataSets pendingFoodReportData = new DashboardDataSets
            {
                Label = "Pending food report"
            };
            // get approved food report each date
            DashboardDataSets approvedFoodReportData = new DashboardDataSets
            {
                Label = "Approved food report"
            };
            // get not approved food report each date
            DashboardDataSets notApprovedFoodReportData = new DashboardDataSets
            {
                Label = "Not approved food report"
            };

            int numberOfDays = (dateEnd - dateFrom).Days + 1;

            DateTime currentDate = dateFrom; // Ngày hiện tại

            for (int i = 0; i < numberOfDays; i++)
            {
                if (currentDate.Date <= dateEnd.Date)
                {
                    // get amount of food of the current date
                    //var currentDateFoodAmount = foods.Where(p => p.CreatedDate.Value.Date == currentDate.Date).Count();
                    //foodData.Data.Add(currentDateFoodAmount);

                    // get amount of food report of the current date
                    var currentDateFoodReportAmount = foodReports.Where(fr => fr.CreateDate.Date == currentDate.Date).Count();
                    foodReportData.Data.Add(currentDateFoodReportAmount);

                    // get amount of pending food report of the current date
                    var currentDatePendingFoodReportAmount = foodReports.Where(fr => fr.CreateDate.Date == currentDate.Date && fr.Status == 0).Count();
                    pendingFoodReportData.Data.Add(currentDatePendingFoodReportAmount);

                    // get list of current date food reports that are approved/not approved
                    var approvedNotApprovedFoodReportCurrentDate = foodReports.Where(fr => fr.UpdateDate != null && fr.UpdateDate.Value.Date == currentDate.Date).ToList();

                    // get amount of approved food report of the current date
                    var currentDateApprovedFoodReportAmount = approvedNotApprovedFoodReportCurrentDate.Where(fr => fr.Status == 1).Count();
                    approvedFoodReportData.Data.Add(currentDateApprovedFoodReportAmount);

                    // get amount of not approved food report of the current date
                    var currentDateNotApprovedFoodReportAmount = approvedNotApprovedFoodReportCurrentDate.Where(fr => fr.Status == 2).Count();
                    notApprovedFoodReportData.Data.Add(currentDateNotApprovedFoodReportAmount);
                }
                else
                {
                    break;
                }

                currentDate = currentDate.AddDays(1);
            }

            //dataSets.Add(foodData);
            dataSets.Add(foodReportData);
            dataSets.Add(pendingFoodReportData);
            dataSets.Add(approvedFoodReportData);
            dataSets.Add(notApprovedFoodReportData);

            return dataSets;
        }
    }
}
