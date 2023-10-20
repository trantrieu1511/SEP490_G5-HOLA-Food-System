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
    public class EnableDisableSellerController : BaseController
    {
        public EnableDisableSellerController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/enableDisableSeller")]
        public BaseOutputDto EnableDisablePost(PostEnableDisableInputDto input)
        {
            try
            {
              
                var business = this.GetBusinessLogic<EnableDisablePostBusinessLogic>();

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
