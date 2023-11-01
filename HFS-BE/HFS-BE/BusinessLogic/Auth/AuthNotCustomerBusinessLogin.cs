using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
	public class AuthNotCustomerBusinessLogin : BaseBusinessLogic
	{
		public AuthNotCustomerBusinessLogin(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public LoginOutputDto LoginNotCustomer(LoginInPutDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthNotCustomerDao>();
				var daoinput = mapper.Map<LoginInPutDto, AuthDaoInputDto>(inputDto);
				var daooutput = Dao.LoginNotCustomer(daoinput);
				var output = mapper.Map<AuthDaoOutputDto, LoginOutputDto>(daooutput);

				return output;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
