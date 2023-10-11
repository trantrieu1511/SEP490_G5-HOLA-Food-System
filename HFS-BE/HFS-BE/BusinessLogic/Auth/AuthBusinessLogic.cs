using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class AuthBusinessLogic : BaseBusinessLogic
	{
		public AuthBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}
		public LoginOutputDto Login(LoginInPutDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();
				var daoinput = mapper.Map<LoginInPutDto, AuthDaoInputDto>(inputDto);
				var daooutput = Dao.Login(daoinput);
				var output = mapper.Map<AuthDaoOutputDto, LoginOutputDto>(daooutput);

				return output;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<LoginOutputDto> LoginGoogle(string inputDto)
		{
			try
			{
				var dao = this.CreateDao<AuthDao>();
				
				var daooutput =await dao.LoginWithGoogleAsync(inputDto);
				
					var output = mapper.Map<AuthDaoOutputDto, LoginOutputDto>(daooutput);
				
				// output = mapper.Map<AuthOutputDto, LoginOutputDto>(daooutput);

				return output;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
