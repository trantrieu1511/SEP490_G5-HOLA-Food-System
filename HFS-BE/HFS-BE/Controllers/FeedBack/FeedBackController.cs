using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FeedBackCustomer;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.FeedBack
{

	public class FeedBackController : BaseControllerSignalR
	{
        public FeedBackController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("users/addNewFeedback")]
		[Authorize]

		public async Task<BaseOutputDto> AddNewFeedback([FromForm] AddFeedBackControllerInputDto input)
		{
			try
			{

				var business = this.GetBusinessLogic<AddFeedBackBL>();

				var inputBL = mapper.Map<AddFeedBackControllerInputDto, AddFeedBackInputDtoBL>(input);

				inputBL.UserDto = this.GetUserInfor();

				string? sellerId;
				var output = business.AddNewFeedback(inputBL, out sellerId);

                if (output.Success)
                {
                    //notify for customer
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(sellerId).SendAsync("notification");
                }

                return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		[HttpPost("users/getFeedback")]
		

		public ListFeedBackOutputDtoBS GetFeedback(GetFeedBackByFoodIdDaoInputDto input)
		{
			try
			{

				var business = this.GetBusinessLogic<ListFeedBackBL>();


				var output = business.ListFeedBack(input);


				return output;
			}
			catch (Exception)
			{
				return this.Output<ListFeedBackOutputDtoBS>(Constants.ResultCdFail);
			}
		}
	}
}
