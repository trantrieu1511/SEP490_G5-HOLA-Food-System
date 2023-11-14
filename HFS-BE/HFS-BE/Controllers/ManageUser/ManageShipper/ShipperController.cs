using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.BusinessLogic.ManageUser.ManageShipper;
using HFS_BE.DAO.SellerDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageShipper
{

	public class ShipperController : BaseController
	{
		public ShipperController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("users/listshipperbyseller")]

		public ShipperInforList ListShipper(ShipperInforDtoInputbySellerId input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.ListShipperBySeller(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[HttpPost("users/listshipperbyadmin")]

		public ShipperInforListByAdmin ListShipperbyAdmin()
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.ListShipperbyAdmin();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/listinvitationshipper")]

		public ListInvitationShipperDtoOutput ListInvitationShipper(ShipperInforDtoInputbySellerId input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.ListInvitationShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/invitationshipper")]

		public BaseOutputDto InvitationShipper(InvitationShipperEmailDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.InvitationShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}



		[HttpPost("users/banshipper")]

		public BaseOutputDto Banhipper(BanShipperDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.BanShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/activeshipper")]

		public BaseOutputDto AciveShipper(ActiveShipperDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.ActiveShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/banshipperhistory")]

		public BaseOutputDto ListHistoryBanShipper(BanShipperHistoryDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.ListHistoryBanShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
