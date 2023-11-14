using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.CommentNewFeed;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace HFS_BE.Controllers.CommentNewFeed
{
    public class GetCommentController : BaseController
    {
        public GetCommentController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("newfeed/getcomment")]
        public GetCommentByPostOutputDto GetComment(GetCommentInputDto inputDto)
        {
            try
            {

                var business = this.GetBusinessLogic<GetCommentByPostBusinessLogic>();

                var output = business.GetComment(inputDto);

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetCommentByPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
