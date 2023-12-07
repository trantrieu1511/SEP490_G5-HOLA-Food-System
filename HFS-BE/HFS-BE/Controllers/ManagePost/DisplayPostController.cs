using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{

    public class DisplayPostController : BaseController
    {
        public DisplayPostController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        //[HttpGet("posts/getPostsSeller")]
        [HttpGet("posts/getPosts")]
        [Authorize]
        public ListPostOutputSellerDto DisplayPost()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayPostBusinessLogic>();
                return business.ListPosts(this.GetUserInfor());
                //return business.ListPosts(new UserDto { UserId = "PM000000001"});
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
