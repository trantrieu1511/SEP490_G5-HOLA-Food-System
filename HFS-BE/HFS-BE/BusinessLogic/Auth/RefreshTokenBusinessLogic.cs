using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using System.IdentityModel.Tokens.Jwt;
using Twilio.Jwt.AccessToken;

namespace HFS_BE.BusinessLogic.Auth
{
    public class RefreshTokenBusinessLogic : BaseBusinessLogic
    {
        public RefreshTokenBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public TokenApiModelOutput Refresh(TokenApiModelBL inputDto) 
        {
            try
            {
                var userDao = CreateDao<UserDao>();
                var authDao = CreateDao<AuthDao>();
                var authNotCus = this.CreateDao<AuthNotCustomerDao>();

                string accessToken = inputDto.AccessToken;
                string refreshToken = inputDto.RefreshToken;

                var email = inputDto.UserDto.Email; //this is mapped to the Name claim by default
                var user = userDao.GetUserRefreshToken(inputDto.UserDto.Email, inputDto.UserDto.Role);

                if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return Output<TokenApiModelOutput>(Constants.ResultCdFail, "Invalid client request");
                string newAccessToken = "";
                switch (inputDto.UserDto.Role)
                {
                    case "CU":

                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authDao.GenerateSecurityToken(new LoginGoogleInputDto
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserId = user.Id
                        }));
                        break;
                    case "SE":
                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authNotCus.GenerateSecurityTokenSeller(new Seller
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            SellerId = user.Id
                        }));
                        break;
                    case "SH":
                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authNotCus.GenerateSecurityTokenShipper(new Shipper
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ShipperId = user.Id
                        }));
                        break;
                    case "AD":
                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authNotCus.GenerateSecurityTokenAdmin(new Models.Admin
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            AdminId = user.Id
                        }));
                        break;
                    case "PM":
                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authNotCus.GenerateSecurityTokenModerator(new PostModerator
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ModId = user.Id
                        }));
                        break;
                    case "MM":
                        newAccessToken = new JwtSecurityTokenHandler().WriteToken(authNotCus.GenerateSecurityTokenModerator(new MenuModerator
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ModId = user.Id
                        }));
                        break;
                }

                var newRefreshToken = authDao.GenerateRefreshToken();

                userDao.EditRefreshToken(new UpdateRefreshToken
                {
                    RefreshToken = newRefreshToken,
                    UserId = user.Id
                });

                var output = Output<TokenApiModelOutput>(Constants.ResultCdSuccess);
                output.RefreshToken = newRefreshToken;
                output.Token = newAccessToken;
                return output;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
