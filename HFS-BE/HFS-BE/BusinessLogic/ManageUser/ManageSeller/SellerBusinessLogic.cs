﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageUser.ManageSeller
{
	public class SellerBusinessLogic : BaseBusinessLogic
	{
		public SellerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListSellerDtoOutput ListSeller()
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();
				var daooutput = Dao.GetAllSeller();

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
