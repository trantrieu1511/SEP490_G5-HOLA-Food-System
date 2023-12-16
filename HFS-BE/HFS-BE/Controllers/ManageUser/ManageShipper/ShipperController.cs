using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.BusinessLogic.ManageUser.ManageShipper;
using HFS_BE.DAO.SellerDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "SE")]
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
		//[Authorize(Roles = "AD")]
		public ListShipperbyAdminOutputDtoBS ListShipperbyAdmin()
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
		[Authorize(Roles = "SE")]
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
		[HttpPost("users/listinvitationshipperbyshipper")]
		[Authorize(Roles = "SH")]
		public ListInvitationShipperbyShipperDtoOutput ListInvitationShipperByShipper()
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				ListInvitationShipperDtoInput input = new ListInvitationShipperDtoInput();
				input.ShipperId = GetUserInfor().UserId;
                var output = business.ListInvitationShipperbyShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/invitationshipper")]
		[Authorize(Roles = "SE")]
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

		[HttpPost("users/acceptshipper")]
		[Authorize(Roles = "SH")]
		public BaseOutputDto AcceptShipper(InvitationShipperDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.AcceptShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[HttpPost("users/dashboardshipper")]
		[Authorize(Roles = "SH")]
		public List<DashBoardShipperOutputDto> DashboardShipper(DashBoardShipperInputDto input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				input.ShipperId = this.GetUserInfor().UserId;
				var output = business.DashBoardShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/dashboardshippertotal")]
		[Authorize(Roles = "SH")]
		public DashBoardShipperTotalOutputDto DashboardShipperTotal()
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				DashBoardShipperTotalInputDto input = new DashBoardShipperTotalInputDto();
				input.ShipperId = this.GetUserInfor().UserId;
				var output = business.DashBoardShipperTotal(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		//[HttpPost("users/banshipper")]
		//[Authorize(Roles = "AD")]
		//public BaseOutputDto Banhipper(BanShipperDtoInput input)
		//{
		//	try
		//	{
		//		var business = this.GetBusinessLogic<ShipperBusinessLogic>();

		//		var output = business.BanShipper(input);
		//		return output;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw;
		//	}
		//}
		[HttpPost("users/activeshipper")]
		[Authorize(Roles = "AD")]
		public async Task<BaseOutputDto> AciveShipper(ActiveShipperDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output =await business.ActiveShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/kickshipper")]
		[Authorize(Roles = "SE")]
		public BaseOutputDto KickShipper(KickShipperDtoInput input)
		{
			try
			{
				var business = this.GetBusinessLogic<ShipperBusinessLogic>();
				var output = business.KickShipper(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		//[HttpPost("users/banshipperhistory")]
		//[Authorize(Roles = "AD")]
		//public BaseOutputDto ListHistoryBanShipper(BanShipperHistoryDtoInput input)
		//{
		//	try
		//	{
		//		var business = this.GetBusinessLogic<ShipperBusinessLogic>();
		//		var output = business.ListHistoryBanShipper(input);
		//		return output;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw;
		//	}
		//}
	}
}
