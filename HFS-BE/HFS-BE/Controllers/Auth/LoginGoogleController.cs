using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{

	public class LoginGoogleController : BaseController
	{
		public LoginGoogleController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("home/logingoogle")]
		
		public async Task<LoginOutputDto> Login(string inputDto)
		{
			try
			{ 
				var business = this.GetBusinessLogic<AuthBusinessLogic>();

				return await business.LoginGoogle(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
