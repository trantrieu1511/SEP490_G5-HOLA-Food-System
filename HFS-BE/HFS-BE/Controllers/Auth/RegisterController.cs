using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{
	public class RegisterController : BaseController
	{
		public RegisterController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		[HttpPost("home/register")]
		public BaseOutputDto Login(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return business.Register(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
