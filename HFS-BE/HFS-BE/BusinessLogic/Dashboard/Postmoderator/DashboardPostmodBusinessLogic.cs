using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Dashboard;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Dashboard.Postmoderator
{
    public class DashboardPostmodBusinessLogic : BaseBusinessLogic
    {
        public DashboardPostmodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DashboardPostModStatisticOutput GetAllTimeStatistics(string userId)
        {
            try
            {
                var dao = CreateDao<PostDao>();
                return dao.GetAllTimeStatistics(userId);
            }
            catch (Exception)
            {
                return Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail);
            }
        }

        //public DashboardPostModStatisticOutput GetThisMonthStatistics(string userId)
        //{
        //    try
        //    {
        //        var dao = CreateDao<PostDao>();
        //        return dao.GetThisMonthStatistics(userId);
        //    }
        //    catch (Exception)
        //    {
        //        return Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail);
        //    }
        //}

        /// <summary>
        /// Get the total of approved and not approve post reports of the post moderator
        /// </summary>
        /// <param name="inputDto">Including DateFrom, DateEnd and ModId</param>
        /// <returns>DashboardPostmodOutput</returns>
        public DashboardPostmodOutput GetMyStatisticsFromDateRange(DAO.PostReportDao.DashboardPostModInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<PostReportDao>();
                var postReportList = dao.GetAllPostReportsByModId(inputDto);
                if (!postReportList.Success)
                {
                    return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
                }

                var output = Output<DashboardPostmodOutput>(Constants.ResultCdSuccess);

                /*
                 * 0. get total approve/not approve post report follow range Date input
                 * 1. datefrom == dateEnd -> ko can lay labels va dataSets
                    return 
                 * 2. datefrom != dateEnd -> get labels and dataSets
                */

                // 0. get count
                // cal count in range
                //var (totalApproved, totalNotApproved) = CountInRange(postReportList.PostReports.Where(
                //        pr => pr.UpdateDate.Value.Date >= inputDto.DateFrom.Value.Date &&
                //        pr.UpdateDate.Value.Date <= inputDto.DateEnd.Value.Date
                //    ).ToList());
                //set statistics

                output.TotalApprovedPostReports = postReportList.PostReports
                    .Where(pr => pr.UpdateDate != null && pr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && pr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && pr.Status == 1)
                    .Count();
                output.TotalNotApprovedPostReports = postReportList.PostReports
                    .Where(pr => pr.UpdateDate != null && pr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && pr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && pr.Status == 2)
                    .Count();

                // 1. datefrom == dateEnd
                if (inputDto.DateFrom.Equals(inputDto.DateEnd))
                {
                    return output;
                }

                // 2.datefrom != dateEnd -> get labels and dataSets

                output.Labels = CalculateLabels((DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);
                output.Datasets = CalculateMyDataSets(postReportList.PostReports, (DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);

                return output;
            }
            catch (Exception)
            {
                return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
            }
        }

        public DashboardPostmodOutput GetSystemStatisticsFromDateRange(DAO.PostReportDao.DashboardPostModInputDto inputDto)
        {
            try
            {
                var postReportDao = CreateDao<PostReportDao>();
                var postDao = CreateDao<PostDao>();

                var postReportList = postReportDao.GetAllPostReports();
                if (!postReportList.Success)
                {
                    return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
                }

                var postList = postDao.GetAllPosts();
                if (!postList.Success)
                {
                    return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
                }

                var output = Output<DashboardPostmodOutput>(Constants.ResultCdSuccess);

                /*
                 * 0. get system statistics follow range Date input
                 * 1. datefrom == dateEnd -> ko can lay labels va dataSets
                    return 
                 * 2. datefrom != dateEnd -> get labels and dataSets
                */

                // 0. get count
                // cal count in range
                //var (totalApproved, totalNotApproved) = CountInRange(postReportList.PostReports.Where(
                //        pr => pr.UpdateDate.Value.Date >= inputDto.DateFrom.Value.Date &&
                //        pr.UpdateDate.Value.Date <= inputDto.DateEnd.Value.Date
                //    ).ToList());
                //set statistics

                output.TotalPosts = postList.Posts
                    .Where(p => p.CreatedDate.Value.Date >= inputDto.DateFrom
                    && p.CreatedDate.Value.Date <= inputDto.DateEnd)
                    .Count();
                output.TotalBannedPosts = postList.Posts
                    .Where(p => p.BanDate != null
                    && p.BanDate.Value.Date >= inputDto.DateFrom
                    && p.BanDate.Value.Date <= inputDto.DateEnd
                    && p.Status == 3)
                    .Count();
                output.TotalPostReports = postReportList.PostReports
                    .Where(pr => pr.CreateDate.Date >= inputDto.DateFrom
                    && pr.CreateDate.Date <= inputDto.DateEnd)
                    .Count();
                output.TotalPendingPostReports = postReportList.PostReports
                    .Where(pr => pr.CreateDate.Date >= inputDto.DateFrom
                    && pr.CreateDate.Date <= inputDto.DateEnd
                    && pr.Status == 0)
                    .Count();
                output.TotalApprovedPostReports = postReportList.PostReports
                    .Where(pr => pr.UpdateDate != null && pr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && pr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && pr.Status == 1)
                    .Count();
                output.TotalNotApprovedPostReports = postReportList.PostReports
                    .Where(pr => pr.UpdateDate != null && pr.UpdateDate.Value.Date >= inputDto.DateFrom
                    && pr.UpdateDate.Value.Date <= inputDto.DateEnd
                    && pr.Status == 2)
                    .Count();

                // 1. datefrom == dateEnd
                if (inputDto.DateFrom.Equals(inputDto.DateEnd))
                {
                    return output;
                }

                // 2.datefrom != dateEnd -> get labels and dataSets

                output.Labels = CalculateLabels((DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);
                output.Datasets = CalculateSystemDataSets(postList.Posts, postReportList.PostReports, (DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);

                return output;
            }
            catch (Exception)
            {
                return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
            }
        }

        //(int totalApproved, int totalNotApproved) CountInRange(List<PostReport> postReports)
        //{
        //    var output = (totalApproved: 0, totalNotApproved: 0);
        //    // Total approve post reports
        //    output.totalApproved = postReports.Where(pr => pr.Status == 1).Count();

        //    // Total not approve post reports
        //    output.totalNotApproved = postReports.Where(pr => pr.Status == 2).Count();

        //    return output;
        //}

        ICollection<string> CalculateLabels(DateTime dateFrom, DateTime dateEnd)
        {
            int numberOfDays = (dateEnd - dateFrom).Days + 1; // Số ngày trong khoảng thời gian

            /*int spaceDateLabel = 0;
            if(numberOfDays > 10)
            {
                spaceDateLabel = numberOfDays / 10;
            }*/

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

                //currentDate = spaceDateLabel == 0 ? currentDate.AddDays(1) : currentDate.AddDays(spaceDateLabel);
                currentDate = currentDate.AddDays(1);
            }

            return dateList;
        }

        /// <summary>
        /// Calculate to get the post moderator's own statistics
        /// </summary>
        /// <param name="postReports">List of post reports</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateEnd">Date to</param>
        /// <returns>ICollection<DashboardDataSets></returns>
        ICollection<DashboardDataSets> CalculateMyDataSets(List<PostReport> postReports, DateTime dateFrom, DateTime dateEnd)
        {
            List<DashboardDataSets> dataSets = new List<DashboardDataSets>();
            // get approved post report each date
            DashboardDataSets approvedPostReportData = new DashboardDataSets
            {
                Label = "Approved post report"
            };
            // get not approved post report each date
            DashboardDataSets notApprovedPostReportData = new DashboardDataSets
            {
                Label = "Not approved post report"
            };

            int numberOfDays = (dateEnd - dateFrom).Days + 1;

            DateTime currentDate = dateFrom; // Ngày hiện tại

            for (int i = 0; i < numberOfDays; i++)
            {
                if (currentDate.Date <= dateEnd.Date)
                {
                    // get list of current date post reports that are approved/not approved
                    var approvedNotApprovedPostReportCurrentDate = postReports.Where(pr => pr.UpdateDate.Value.Date == currentDate.Date).ToList();

                    // get amount of approved post report of the current date
                    var currentDateApprovedPostReportAmount = approvedNotApprovedPostReportCurrentDate.Where(pr => pr.Status == 1).Count();
                    approvedPostReportData.Data.Add(currentDateApprovedPostReportAmount);

                    // get amount of not approved post report of the current date
                    var currentDateNotApprovedPostReportAmount = approvedNotApprovedPostReportCurrentDate.Where(pr => pr.Status == 2).Count();
                    notApprovedPostReportData.Data.Add(currentDateNotApprovedPostReportAmount);
                }
                else
                {
                    break;
                }

                currentDate = currentDate.AddDays(1);
            }

            dataSets.Add(approvedPostReportData);
            dataSets.Add(notApprovedPostReportData);

            return dataSets;
        }

        /// <summary>
        /// Calculate to get the system statistics that relates to the post moderator role
        /// </summary>
        /// <param name="postReports">List of post reports</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateEnd">Date to</param>
        /// <returns>ICollection<DashboardDataSets></returns>
        ICollection<DashboardDataSets> CalculateSystemDataSets(List<Post> posts, List<PostReport> postReports, DateTime dateFrom, DateTime dateEnd)
        {
            List<DashboardDataSets> dataSets = new List<DashboardDataSets>();

            // get no of post each date
            DashboardDataSets postData = new DashboardDataSets
            {
                Label = "Post"
            };
            // get no of banned post each date
            DashboardDataSets bannedPostData = new DashboardDataSets
            {
                Label = "Banned post"
            };
            // get no of post reports each date
            DashboardDataSets postReportData = new DashboardDataSets
            {
                Label = "Post report"
            };
            // get no of pending post report each date
            DashboardDataSets pendingPostReportData = new DashboardDataSets
            {
                Label = "Pending post report"
            };
            // get approved post report each date
            DashboardDataSets approvedPostReportData = new DashboardDataSets
            {
                Label = "Approved post report"
            };
            // get not approved post report each date
            DashboardDataSets notApprovedPostReportData = new DashboardDataSets
            {
                Label = "Not approved post report"
            };

            int numberOfDays = (dateEnd - dateFrom).Days + 1;

            DateTime currentDate = dateFrom; // Ngày hiện tại

            for (int i = 0; i < numberOfDays; i++)
            {
                if (currentDate.Date <= dateEnd.Date)
                {
                    // get amount of post of the current date
                    var currentDatePostAmount = posts.Where(p => p.CreatedDate.Value.Date == currentDate.Date).Count();
                    postData.Data.Add(currentDatePostAmount);

                    // get amount of banned post of the current date
                    var currentDateBannedPostAmount = posts.Where(p => p.BanDate != null && p.BanDate.Value.Date == currentDate.Date && p.Status == 3).Count();
                    bannedPostData.Data.Add(currentDateBannedPostAmount);

                    // get amount of post report of the current date
                    var currentDatePostReportAmount = postReports.Where(pr => pr.CreateDate.Date == currentDate.Date).Count();
                    postReportData.Data.Add(currentDatePostReportAmount);

                    // get amount of pending post report of the current date
                    var currentDatePendingPostReportAmount = postReports.Where(pr => pr.CreateDate.Date == currentDate.Date && pr.Status == 0).Count();
                    pendingPostReportData.Data.Add(currentDatePendingPostReportAmount);

                    // get list of current date post reports that are approved/not approved
                    var approvedNotApprovedPostReportCurrentDate = postReports.Where(pr => pr.UpdateDate != null && pr.UpdateDate.Value.Date == currentDate.Date).ToList();

                    // get amount of approved post report of the current date
                    var currentDateApprovedPostReportAmount = approvedNotApprovedPostReportCurrentDate.Where(pr => pr.Status == 1).Count();
                    approvedPostReportData.Data.Add(currentDateApprovedPostReportAmount);

                    // get amount of not approved post report of the current date
                    var currentDateNotApprovedPostReportAmount = approvedNotApprovedPostReportCurrentDate.Where(pr => pr.Status == 2).Count();
                    notApprovedPostReportData.Data.Add(currentDateNotApprovedPostReportAmount);
                }
                else
                {
                    break;
                }

                currentDate = currentDate.AddDays(1);
            }

            dataSets.Add(postData);
            dataSets.Add(bannedPostData);
            dataSets.Add(postReportData);
            dataSets.Add(pendingPostReportData);
            dataSets.Add(approvedPostReportData);
            dataSets.Add(notApprovedPostReportData);

            return dataSets;
        }
    }
}
