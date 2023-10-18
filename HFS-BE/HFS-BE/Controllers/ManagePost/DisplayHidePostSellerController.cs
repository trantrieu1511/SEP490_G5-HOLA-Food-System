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
    public class DisplayHidePostSellerController : BaseController
    {
        public DisplayHidePostSellerController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/displayHideSeller")]
        public BaseOutputDto DisplayHidePost(PostDisplayHideInputDto input)
        {
            try
            {
              
                var business = this.GetBusinessLogic<DisplayHidePostBusinessLogic>();

                var output = business.DisplayHidePost(input);

                // call signalR 

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
