using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageCustomer
{

	public class CustomersController : BaseController
	{
		public CustomersController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		[HttpPost("users/listcustomer")]
	//	[Authorize(Roles = "AD")]
		public ListCustomerOutputDtoBS ListCustomer()
		{
			try
			{
				var business = this.GetBusinessLogic<CustomerBusinessLogic>();
				var output = business.ListCustomer();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		//[HttpPost("users/bancustomer")]
		//[Authorize(Roles = "AD")]
		//public BaseOutputDto BanCustomer(BanCustomerDtoInput input)
		//{
		//	try
		//	{
		//		var business = this.GetBusinessLogic<CustomerBusinessLogic>();
		//		var output = business.BanCustomer(input);
		//		return output;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw;
		//	}
		//}
		//[HttpPost("users/bancustomerhistory")]
		//[Authorize(Roles = "AD")]
		//public BaseOutputDto ListHistoryBanCustomer(BanCustomerHistoryDtoInput input)
		//{
		//	try
		//	{
		//		var business = this.GetBusinessLogic<CustomerBusinessLogic>();
		//		var output = business.ListHistoryBanCustomer(input);
		//		return output;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw;
		//	}
		//}
	}
}
