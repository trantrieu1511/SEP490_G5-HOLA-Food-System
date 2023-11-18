using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Auth
{
    public class RevokeTokenBusinessLogic : BaseBusinessLogic
    {
        public RevokeTokenBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto RevokeToken(RevokeToken inputDto)
        {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.RevokeToken(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
