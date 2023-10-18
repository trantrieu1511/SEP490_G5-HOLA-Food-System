using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Post;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{

    public class DisplayPostSellerController : BaseController
    {
        public DisplayPostSellerController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("posts/getPostSeller")]
        public ListPostOutputSellerDto Get()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayPostSellerBusinessLogic>();

                return business.ListPosts(this.GetUserInfor());
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
