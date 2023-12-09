using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.CommentNewFeed;
using HFS_BE.BusinessLogic.ManageVoucher;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.CommentNewFeed
{
    public class CreateCommentController : BaseController
    {
        public CreateCommentController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("users/comment")]
        //[Authorize]

        public BaseOutputDto CreateComment( CreateCommentInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<CreateCommentBusinessLogic>();

                var output = business.CreateComment(input);

                return output;
            }
            catch (Exception)
            {
                return this.Output<CreateVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
