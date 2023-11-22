using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ReplyFeedBack;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace HFS_BE.Controllers.ReplyFeedBack
{

	public class ReplyFeedBackController : BaseController
	{
		public ReplyFeedBackController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
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
		public BaseOutputDto Reply(CreateFeedBackbySellerDaoInputDto input)
		{
			try
			{

				var business = this.GetBusinessLogic<ReplyFeedBackBL>();
			
				var output = business.Reply(input);


				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
