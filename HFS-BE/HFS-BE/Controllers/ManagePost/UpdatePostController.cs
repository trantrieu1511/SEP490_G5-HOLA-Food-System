using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{
    public class UpdatePostController : BaseController
    {
        public UpdatePostController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPut("posts/updatePost")]
        public BaseOutputDto UpdatePost([FromForm] PostUpdateInputDto input)
        {
            try
            {
                var business = this.GetBusinessLogic<UpdatePostBusinessLogic>();

                var inputBL = mapper.Map<PostUpdateInputDto, BusinessLogic.ManagePost.PostUpdateInputDto>(input);

                inputBL.UserDto = this.GetUserInfor();

                var output = business.UpdatePost(inputBL);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
