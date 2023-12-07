using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using HFS_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{

	public class LoginGoogleController : BaseControllerAuth
	{
        public LoginGoogleController(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService) : base(context, mapper, tokenService)
        {
        }

        [HttpPost("home/logingoogle")]
		public async Task<LoginOutputDto> Login([FromBody]string inputDto)
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
