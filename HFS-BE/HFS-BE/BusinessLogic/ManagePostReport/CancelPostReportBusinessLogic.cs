using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class CancelPostReportBusinessLogic : BaseBusinessLogic
    {
        public CancelPostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        //public BaseOutputDto CancelPostReport(CancelPostReportInputDto inputDto) {
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {
        //        return Output<BaseOutputDto>(Constants.ResultCdFail);
        //    }
        //}
    }
}
