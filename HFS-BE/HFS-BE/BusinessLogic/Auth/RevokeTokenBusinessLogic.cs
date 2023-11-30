using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Auth;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using static HFS_BE.BusinessLogic.Auth.RegisterSellerInputDto;

namespace HFS_BE.BusinessLogic.Auth
{
    public class RevokeTokenBusinessLogic : BaseBusinessLogic
    {
        public RevokeTokenBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto RevokeToken(TokenApiModel tokenApiModel)
        {
            try
            {
                var dao = CreateDao<UserDao>();
                var userDao = CreateDao<UserDao>();

                var user = userDao.GetUserRefreshToken(tokenApiModel.RefreshToken);
                if(user is null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail);
                }
                return dao.RevokeToken(new Auth.RevokeToken
                {
                    Id = user.Id,
                    Role = user.Id.Substring(0, 2)
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
