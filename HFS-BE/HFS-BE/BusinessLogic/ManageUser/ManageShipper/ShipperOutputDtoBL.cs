using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;

namespace HFS_BE.BusinessLogic.ManageUser.ManageShipper
{
	public class ShipperOutputDtoBL
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
		public byte Status { get; set; }
		public DateTime? CreateDate { get; set; }
		public string? Note { get; set; }
		public bool? IsPhoneVerified { get; set; }
		public List<ShipperImageOutputDto>? ImagesBase64 { get; set; } = new List<ShipperImageOutputDto>();
	}
	public class ShipperImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}

	public class ListShipperbyAdminOutputDtoBS : BaseOutputDto
	{
		public List<ShipperOutputDtoBL> Shippers { get; set; }
	}
}
