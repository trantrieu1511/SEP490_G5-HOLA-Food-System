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
		public async Task<BaseOutputDto> RegisterCustomer(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return await business.Register(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/registerseller")]
		public async Task<BaseOutputDto> RegisterSeller(RegisterSellerInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return await business.RegisterSeller(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/registershipper")]
		public async Task<BaseOutputDto> RegisterShipper(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<RegisterBusinessLogic>();

				return await business.RegisterShipper(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
