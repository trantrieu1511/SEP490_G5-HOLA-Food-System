using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{
	public class LoginController : BaseController
	{
		public LoginController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}

		[HttpPost("home/login")]
		public LoginOutputDto Login(LoginInPutDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<AuthBusinessLogic>();

				return business.Login(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
