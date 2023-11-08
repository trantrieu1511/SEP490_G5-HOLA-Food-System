using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class ApproveNotApprovePostReportBusinessLogic : BaseBusinessLogic
    {
        public ApproveNotApprovePostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ApproveNotApprovePostReport(ApproveNotApprovePostReportInputDto inputDto, string updateBy)
        {
            try
            {
                var dao = CreateDao<PostReportDao>();
                return dao.ApproveNotApprovePostReport(inputDto, updateBy);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
