using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageMenuReport
{
    public class ApproveNotApproveFoodReportBusinessLogic : BaseBusinessLogic
    {
        public ApproveNotApproveFoodReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ApproveNotApproveFoodReport(ApproveNotApproveFoodReportInputDto inputDto, string updateBy)
        {
            try
            {
                var dao = CreateDao<FoodReportDao>();
                return dao.ApproveNotApproveFoodReport(inputDto, updateBy);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
