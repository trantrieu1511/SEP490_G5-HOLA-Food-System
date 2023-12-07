using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class ForgotPasswordBusinessLogic : BaseBusinessLogic
	{
		public ForgotPasswordBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
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

		public BaseOutputDto ConfirmForgotPassword(ConfirmForgotPasswordInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();

				var daooutput =  Dao.ValidateConfirmationCodeForgot(inputDto);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto ChangeForgotPassword(ChangeForgotPasswordInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();

				var daooutput = Dao.ChangePassword(inputDto);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
