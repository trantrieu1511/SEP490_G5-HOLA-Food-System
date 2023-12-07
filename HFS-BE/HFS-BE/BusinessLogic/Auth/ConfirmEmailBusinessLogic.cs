using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class ConfirmEmailBusinessLogic : BaseBusinessLogic
	{
		public ConfirmEmailBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public async Task<BaseOutputDto> SendConfirmEmail(ForgotPasswordInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();

				var daooutput =await Dao.SendVetifyPasswordtoEmailAsync(inputDto);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto ConfirmEmail(ConfirmForgotPasswordInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();

				var daooutput =Dao.ValidateConfirmationCode(inputDto);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
