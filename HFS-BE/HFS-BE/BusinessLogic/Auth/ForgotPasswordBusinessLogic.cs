using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class ForgotPasswordBusinessLogic : BaseBusinessLogic
	{
		public ForgotPasswordBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}
		public async Task<BaseOutputDto> SendForgotPassword(ForgotPasswordInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();

				var daooutput = await Dao.SendForgotPasswordtoEmailAsync(inputDto);
			
				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
