using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageMenuReport
{
    public class CreateNewFoodReportBusinessLogic : BaseBusinessLogic
    {
        public CreateNewFoodReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateNewFoodReport(CreateNewFoodReportInputDto inputDto, string reportBy)
        {
            try
            {
                var dao = CreateDao<FoodReportDao>();
                return dao.CreateNewFoodReport(inputDto, reportBy);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
