using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AdminDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.Models;

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
	}
}
