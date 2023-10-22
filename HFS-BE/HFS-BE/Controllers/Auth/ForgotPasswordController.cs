using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{
	
	public class ForgotPasswordController : BaseController
	{
		public ForgotPasswordController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("home/sendforgot")]
		public async Task<BaseOutputDto> Forgot(ForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return await business.SendForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/confirmforgot")]
		public BaseOutputDto ConfirmForgot(ConfirmForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return  business.ConfirmForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/changepassword")]
		public BaseOutputDto ChangeForgot(ChangeForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return business.ChangeForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
