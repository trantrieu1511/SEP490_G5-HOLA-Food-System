using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageSeller
{

	public class SellerController : BaseController
	{
		public SellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("users/listseller")]

		public ListSellerOutputDtoBS ListSeller()
		{
			try
			{
				var business = this.GetBusinessLogic<SellerBusinessLogic>();
				var output = business.ListSeller();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/banseller")]

		public BaseOutputDto BanSeller(BanSellerDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<SellerBusinessLogic>();
				var output = business.BanSeller(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/activeseller")]

		public BaseOutputDto AciveSeller(ActiveSellerDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<SellerBusinessLogic>();
				var output = business.ActiveSeller(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/bansellerhistory")]

		public BaseOutputDto ListHistoryBanSeller(BanSellerHistoryDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<SellerBusinessLogic>();
				var output = business.ListHistoryBanSeller(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
