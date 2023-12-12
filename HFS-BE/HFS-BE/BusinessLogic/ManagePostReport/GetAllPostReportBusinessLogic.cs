using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class GetAllPostReportBusinessLogic : BaseBusinessLogic
    {
        public GetAllPostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostReportOutputDto GetAllPostReports(string userId)
        {
            try
            {
                var dao = CreateDao<PostReportDao>();
                return dao.GetAllPostReportsByUserId(userId);
            }
            catch (Exception)
            {
                return Output<ListPostReportOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
