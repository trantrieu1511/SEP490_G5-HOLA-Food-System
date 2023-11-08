using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Newsfeed;
using HFS_BE.DAO.NewsfeedDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Newsfeed
{
    public class DisplayNewsfeedController : BaseController
    {
        public DisplayNewsfeedController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("newsfeed/getAllNews")]
        public BusinessLogic.Newsfeed.ListNewsfeedOutputDto GetAllNews()
        {
            try
            {
                /*if (!GetUserInfor().UserId.Substring(0, 2).Equals("CU") && GetUserInfor().UserId.Substring(0, 2) != null)
                {
                    return Output<BusinessLogic.Newsfeed.ListNewsfeedOutputDto>(Constants.ResultCdFail, "Please sign in as a customer or sign out as a guest to use this API.");
                }*/
                var business = GetBusinessLogic<DisplayNewsfeedBusinessLogic>();
                return business.GetAllNews();
            }
            catch (Exception e)
            {
                return Output<BusinessLogic.Newsfeed.ListNewsfeedOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }
    }
}
