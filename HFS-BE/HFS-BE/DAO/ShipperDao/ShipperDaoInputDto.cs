using HFS_BE.Base;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.ShipperDao
{
	public class ShipperInforDtoInputbySellerId:BaseInputDto
	{
		public string? ManageBy { get; set; }
	}

	public class InvitationShipperDtoInput : BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public string ShipperId { get; set; } = null!;
		public byte Accepted { get; set; }
	}
	public class KickShipperDtoInput : BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public string ShipperId { get; set; } = null!;
	
	}
	public class ListInvitationShipperDtoInput : BaseInputDto
	{
		public string? ShipperId { get; set; } = null!;

	}
	public class InvitationShipperEmailDtoInput : BaseInputDto
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }
		[Required(ErrorMessage = "SellerId is required")]
		public string SellerId { get; set; }
	

	}
	public class BanShipperDtoInput : BaseInputDto
	{
		public string ShipperId { get; set; } = null!;
		[Required(ErrorMessage = "Reason is required")]
		public string? Reason { get; set; }
		public bool? IsBanned { get; set; }

	}
	public class ActiveShipperDtoInput : BaseInputDto
	{
		public string ShipperId { get; set; } = null!;
		public bool? IsVerified { get; set; }
	}

	public class BanShipperHistoryDtoInput : BaseInputDto
	{
		public string ShipperId { get; set; }
	}
}
