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

		[HttpPost("home/registercustomer")]
		public BaseOutputDto RegisterCustomer(RegisterInputDto inputDto)
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
		[HttpPost("home/registerseller")]
		public BaseOutputDto RegisterSeller(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return business.RegisterSeller(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/registershipper")]
		public BaseOutputDto RegisterShipper(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return business.RegisterShipper(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
