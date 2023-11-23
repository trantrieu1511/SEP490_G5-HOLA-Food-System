using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ReplyFeedBack;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Buffers.Text;

namespace HFS_BE.Controllers.ReplyFeedBack
{

	public class ReplyFeedBackController : BaseControllerSignalR
	{
        public ReplyFeedBackController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("users/sellerFeedBack")]
		public ListFeedBackbySellerOutputDtoBL GetFeedbackbySeller()
		{
			try
			{

				var business = this.GetBusinessLogic<ReplyFeedBackBL>();
				string sellerid =this.GetUserInfor().UserId;
				var output = business.ListFeedBackbySeller(sellerid);


				return output;
			}
			catch (Exception)
			{
				return this.Output<ListFeedBackbySellerOutputDtoBL>(Constants.ResultCdFail);
			}
		}
		[HttpPost("users/replyfeedback")]
		public async Task<BaseOutputDto> Reply(CreateFeedBackbySellerDaoInputDto input)
		{
			try
			{

				var business = this.GetBusinessLogic<ReplyFeedBackBL>();
			
				var output = business.Reply(input);

                if (output.Success)
                {
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(input.CustomerId).SendAsync("notification");
                }

                return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
