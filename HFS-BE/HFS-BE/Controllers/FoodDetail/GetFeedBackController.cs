using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.FoodDetail
{
    public class GetFeedBackController : BaseController
    {
        public GetFeedBackController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("fooddetail/feedbck")]
        public GetFeedBackOutputDto GetFeedBack(GetFeedBackInputDto inputDto)
        {
            try
            {
                var userInfor = this.GetUserInfor();
                inputDto.CustomerId = userInfor != null ? userInfor.UserId : null;
                var busi = this.GetBusinessLogic<GetFeedBackBusinessLogic>();
                return busi.GetFeedBack(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetFeedBackOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
