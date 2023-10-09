using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class RegisterBusinessLogic : BaseBusinessLogic
	{
		public RegisterBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}
		public BaseOutputDto Register(RegisterInputDto inputDto)
		{
			try
			{
				var dao = this.CreateDao<AuthDAO>();
				var daoinput = mapper.Map<RegisterInputDto,RegisterDto>(inputDto);
				var daooutput = dao.Register(daoinput);
			

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
