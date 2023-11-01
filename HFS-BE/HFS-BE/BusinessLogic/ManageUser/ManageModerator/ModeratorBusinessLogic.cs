using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.ManageUser.ManageModerator
{
	public class ModeratorBusinessLogic : BaseBusinessLogic
	{
		public ModeratorBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListPostModeratorDtoOutput ListPostModerator()
		{
			try
			{
				var Dao = this.CreateDao<ModeratorDao>();
				var daooutput = Dao.GetAllPostModerator();

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListPostModeratorDtoOutput ListMenuModerator()
		{
			try
			{
				var Dao = this.CreateDao<ModeratorDao>();
				var daooutput = Dao.GetAllPostModerator();

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto AddPostModerator(RegisterInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<ModeratorDao>();
				var daoinput = mapper.Map<RegisterInputDto, RegisterDto>(inputDto);
				var daooutput = Dao.CreatePostModerator(daoinput);
			

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto AddMenuModerator(RegisterInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<ModeratorDao>();
				var daoinput = mapper.Map<RegisterInputDto, RegisterDto>(inputDto);
				var daooutput = Dao.CreateMenuModerator(daoinput);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
