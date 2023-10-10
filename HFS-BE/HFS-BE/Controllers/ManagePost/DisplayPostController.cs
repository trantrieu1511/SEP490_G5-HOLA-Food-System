using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Post;
using HFS_BE.Dao.PostDao;
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

        [HttpGet("manage/viewposts")]
        public ListPostOutputDto Get()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayPostBusinessLogic>();
                return business.ListPosts();
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
