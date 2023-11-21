using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FeedBackCustomer;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.FeedBack
{

	public class FeedBackController : BaseController
	{
		public FeedBackController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
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

				var output = business.AddNewFeedback(inputBL);

				
				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
