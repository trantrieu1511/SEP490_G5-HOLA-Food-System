using HFS_BE.Base;

namespace HFS_BE.DAO.ShipperDao
{
    public class ShipperInfor
    {
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? Gender { get; set; }
        public string? BirthDate { get; set; }
        public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
    }

    public class ShipperInforList: BaseOutputDto
    {
        public List<ShipperInfor>? Shippers { get; set; }
    }
	public class ShipperInforByAdmin
	{
		public string? ShipperId { get; set; }
		public string? ShipperName { get; set; }
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public string? ManageBy { get; set; }
		public bool? ConfirmedEmail { get; set; }
		public bool? IsBanned { get; set; }
		public bool? IsVerified { get; set; }
		public List<ImageShipperOutputDto>? Images { get; set; }
	}
	public class ImageShipperOutputDto
	{
		public int ImageId { get; set; }
		public string UserId { get; set; } = null!;
		public string Path { get; set; } = null!;
		public bool IsReplaced { get; set; }
	}

	public class ShipperInforListByAdmin : BaseOutputDto
	{
		public List<ShipperInforByAdmin>? Shippers { get; set; }
	}

	public class InvitationShipperDtoOutput
	{
		public string? ShipperId { get; set; }
		public string? ShipperName { get; set; }
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public byte Accepted { get; set; }
	}
	public class InvitationSellerDtoOutput
	{
		public string? SellerId { get; set; }
		public string? SellerName { get; set; }
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public byte Accepted { get; set; }
	}

	public class ListInvitationShipperDtoOutput : BaseOutputDto
	{
		public List<InvitationShipperDtoOutput>? data { get; set; }
	}
	public class ListInvitationShipperbyShipperDtoOutput : BaseOutputDto
	{
		public List<InvitationSellerDtoOutput>? data { get; set; }
	}
	public class BanHistoryShipperDtoOutput : BaseOutputDto
	{
		public int BanShipperId { get; set; }
		public string ShipperId { get; set; }
		public string? Reason { get; set; }
		public DateTime CreateDate { get; set; }
	}

	public class ListHistoryBanShipper : BaseOutputDto
	{
		public List<BanHistoryShipperDtoOutput> data { get; set; }
	}

}
