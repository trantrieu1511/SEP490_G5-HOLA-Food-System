using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageMenuReport
{
    public class DisplayFoodReportBusinessLogic : BaseBusinessLogic
    {
        public DisplayFoodReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListFoodReportOutputDto GetAllFoodReports(string userId)
        {
            try
            {
                var dao = CreateDao<FoodReportDao>();
                return dao.GetAllFoodReportsByUserId(userId);
            }
            catch (Exception)
            {
                return Output<ListFoodReportOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
