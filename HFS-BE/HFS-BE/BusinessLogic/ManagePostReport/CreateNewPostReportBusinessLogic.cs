using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class CreateNewPostReportBusinessLogic : BaseBusinessLogic
    {
        public CreateNewPostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateNewPostReport(CreateNewPostReportInputDto inputDto, string reportBy)
        {
            try
            {
                var dao = CreateDao<PostReportDao>();
                return dao.CreateNewPostReport(inputDto, reportBy);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
