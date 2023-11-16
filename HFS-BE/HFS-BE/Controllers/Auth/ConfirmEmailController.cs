using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{

	public class ConfirmEmailController : BaseController
	{
		public ConfirmEmailController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		[HttpPost("home/sendconfirm")]
		public async Task<BaseOutputDto>Send(ForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ConfirmEmailBusinessLogic>();

				return await business.SendConfirmEmail(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/confirm")]
		public async Task<BaseOutputDto> Confirm(ConfirmForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ConfirmEmailBusinessLogic>();

				return  business.ConfirmEmail(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
