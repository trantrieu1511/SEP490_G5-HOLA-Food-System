﻿using HFS_BE.Base;

namespace HFS_BE.DAO.SellerDao
{
	public class BanSellerDtoInput : BaseInputDto
	{
		public string SellerId { get; set; } = null!;
		public string? Reason { get; set; }
		public bool? IsBanned { get; set; }
	}
}
