﻿using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.DAO.SellerDao;

namespace HFS_BE.BusinessLogic.ManageUser.ManageSeller
{
	public class SellerDtoBS
	{
		public string SellerId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public long? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool IsOnline { get; set; }
		public decimal? WalletBalance { get; set; }
		public string? ShopName { get; set; }
		public string? ShopAddress { get; set; }
		public bool? ConfirmedEmail { get; set; }
		public bool? IsBanned { get; set; }
		public bool? IsVerified { get; set; }
		public List<SellerImageOutputDto>? ImagesBase64 { get; set; } = new List<SellerImageOutputDto>();
	}

	public class SellerImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}

	public class ListSellerOutputDtoBS : BaseOutputDto
	{
		public List<SellerDtoBS> Sellers { get; set; }
	}
}