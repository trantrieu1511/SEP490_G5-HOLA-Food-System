﻿using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;

namespace HFS_BE.DAO.ModeratorDao
{
	public class ListMenuModeratorDtoOutput:BaseOutputDto
	{
		public List<MenuModeratorDtoOutput> data { get; set; }
	}
}
