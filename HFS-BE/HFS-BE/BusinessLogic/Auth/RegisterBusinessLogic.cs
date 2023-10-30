using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class RegisterBusinessLogic : BaseBusinessLogic
	{
		public RegisterBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public BaseOutputDto Register(RegisterInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();
				var daoinput = mapper.Map<RegisterInputDto,RegisterDto>(inputDto);
				//var daooutput = Dao.Register(daoinput);
			

				return null;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
