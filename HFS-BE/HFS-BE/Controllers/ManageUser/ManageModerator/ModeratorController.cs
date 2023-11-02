using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.BusinessLogic.ManageUser;
using HFS_BE.BusinessLogic.ManageUser.ManageModerator;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageModerator
{

	public class ModeratorController : BaseController
	{
		public ModeratorController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("users/listpostmoderator")]

		public ListPostModeratorDtoOutput ListPostModerator()
		{
			try
			{
				var business = this.GetBusinessLogic<ModeratorBusinessLogic>();
				var output = business.ListPostModerator();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/listmenumoderator")]

		public ListPostModeratorDtoOutput ListMenuModerator()
		{
			try
			{
				var business = this.GetBusinessLogic<ModeratorBusinessLogic>();
				var output = business.ListMenuModerator();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/addpostmoderator")]

		public BaseOutputDto AddPostModerator(CreateModerator input)
		{
			try
			{
				var business = this.GetBusinessLogic<ModeratorBusinessLogic>();
				var output = business.AddPostModerator(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("users/addmenumoderator")]

		public BaseOutputDto AddMenuModerator(CreateModerator input)
		{
			try
			{
				var business = this.GetBusinessLogic<ModeratorBusinessLogic>();
				var output = business.AddMenuModerator(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
