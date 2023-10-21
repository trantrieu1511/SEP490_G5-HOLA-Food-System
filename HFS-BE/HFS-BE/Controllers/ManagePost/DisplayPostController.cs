using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{

    public class DisplayPostController : BaseController
    {
        public DisplayPostController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("posts/getPostsSeller")]
        public ListPostOutputSellerDto Get()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayPostBusinessLogic>();

                return business.ListPosts(this.GetUserInfor());
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
