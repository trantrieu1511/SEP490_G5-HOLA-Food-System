using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
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
        
        public DashboardPostModStatisticOutput GetThisMonthStatistics(string userId)
        {
            try
            {
                var dao = CreateDao<PostDao>();
                return dao.GetThisMonthStatistics(userId);
            }
            catch (Exception)
            {
                return Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail);
            }
        }
    }
}
