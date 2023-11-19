using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class VerifyIdentityBusinessLogic : BaseBusinessLogic
    {
        public VerifyIdentityBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// Verify the user's identity before changing the password
        /// </summary>
        /// <returns></returns>
        public BaseOutputDto VerifyUsersIdentity(VerifyIdentityInputDto inputDto, string userId)
        {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.VerifyUsersIdentity(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
