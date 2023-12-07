using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Services;

namespace HFS_BE.BusinessLogic.Auth
{
	public class AuthNotCustomerBusinessLogin : BaseBusinessLogicAuth
	{
        public AuthNotCustomerBusinessLogin(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService) : base(context, mapper, tokenService)
        {
        }

        public LoginOutputDto LoginNotCustomer(LoginInPutDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthNotCustomerDao>();
				var userDao = CreateDao<UserDao>();


				var daoinput = mapper.Map<LoginInPutDto, AuthDaoInputDto>(inputDto);
				var daooutput = Dao.LoginNotCustomer(daoinput);
				var output = mapper.Map<AuthDaoOutputDto, LoginOutputDto>(daooutput);

				if (!output.Success)
				{
					return output;
				}

                var refreshToken = _tokenService.GenerateRefreshToken();

                var refreshTokenInput = new AddRefreshToken
				{
					UserId = daooutput.UserId,
					Role = daooutput.UserId.Substring(0, 2),
                    RefreshToken = refreshToken.Token,
					RefreshTokenExpiryTime = refreshToken.Expired
                };

				var outputAddToken = userDao.AddRefreshToken(refreshTokenInput);

				if (!outputAddToken.Success)
				{
					return output;
				}

                output.RefreshToken = refreshToken;
				return output;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
