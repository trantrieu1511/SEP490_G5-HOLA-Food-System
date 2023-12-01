using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{
    public class EnableDisableSellerController : BaseController
    {
        public EnableDisableSellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/enableDisableSeller")]
        [Authorize]
        public BaseOutputDto EnableDisablePost(PostEnableDisableInputDto input)
        {
            try
            {
              
                var business = this.GetBusinessLogic<EnableDisablePostBusinessLogic>();
                input.UserDto = GetUserInfor();

                var output = business.EnableDisablePost(input);

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
