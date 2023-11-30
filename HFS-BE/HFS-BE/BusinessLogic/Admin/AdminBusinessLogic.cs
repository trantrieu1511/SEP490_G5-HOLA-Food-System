﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AdminDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.Admin
{
	public class AdminBusinessLogic:BaseBusinessLogic
	{
		public AdminBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		public BaseOutputDto RegisterAdmin(RegisterInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AdminDao>();
				var daoinput = mapper.Map<RegisterInputDto, RegisterDto>(inputDto);
				var daooutput = Dao.CreateAdmin(daoinput);


				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public List<DashboadPieAdminOutputDto> GetDashBoadPie()
		{
			try
			{
				var Dao = this.CreateDao<AdminDao>();
			
				var daooutput = Dao.GetDashBoadPie();

				return daooutput;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public DashboadAdminOutputDto GetDashBoadTotal()
		{
			try
			{
				var Dao = this.CreateDao<AdminDao>();

				var daooutput = Dao.GetDashBoadAdminTotal();

				return daooutput;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public List<DashBoardAdminLineOutputDto> DashBoardAdminLine(DashBoardAdminLineInputDto input)
		{
			try
			{
				var Dao = this.CreateDao<AdminDao>();

				var daooutput = Dao.GetDashBoadLine(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
