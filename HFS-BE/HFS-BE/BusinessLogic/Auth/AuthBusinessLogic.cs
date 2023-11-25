using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Auth
{
	public class AuthBusinessLogic : BaseBusinessLogicAuth
	{
        public AuthBusinessLogic(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService) : base(context, mapper, tokenService)
        {
        }

        public LoginOutputDto Login(LoginInPutDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();
                var userDao = CreateDao<UserDao>();

                var daoinput = mapper.Map<LoginInPutDto, AuthDaoInputDto>(inputDto);
				var daooutput = Dao.Login(daoinput);
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

		public async Task<LoginOutputDto> LoginGoogle(string inputDto)
		{
			try
			{
				var dao = this.CreateDao<AuthDao>();
                var userDao = CreateDao<UserDao>();

				var daooutput = await dao.LoginWithGoogleAsync(inputDto);

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
