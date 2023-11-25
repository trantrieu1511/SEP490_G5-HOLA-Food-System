﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Admin;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.DAO.AdminDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Admin
{

	public class AdminController : BaseController
	{
		public AdminController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("home/registeradmin")]

		public BaseOutputDto RegisterAdmin(RegisterInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<AdminBusinessLogic>();

				return business.RegisterAdmin(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/dashboadpie")]
		[Authorize(Roles = "AD")]
		public List<DashboadPieAdminOutputDto> GetDashBoadPie()
		{
			try
			{
				var business = this.GetBusinessLogic<AdminBusinessLogic>();
				return business.GetDashBoadPie();
			}
			catch (Exception ex)
			{

				throw;
			}
		}
		[HttpPost("users/dashboadtotal")]
		[Authorize(Roles = "AD")]
		public DashboadAdminOutputDto GetDashBoadTotal()
		{
			try
			{
				var business = this.GetBusinessLogic<AdminBusinessLogic>();
				return business.GetDashBoadTotal();
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
