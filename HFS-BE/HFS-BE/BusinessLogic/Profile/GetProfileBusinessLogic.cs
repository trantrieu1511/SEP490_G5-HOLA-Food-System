using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class GetProfileBusinessLogic : BaseBusinessLogic
    {
        public GetProfileBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public UserProfileOutputDto GetProfile(string userId) {
            try
            {
                //if (userId == null)
                //{
                //    return Output<UserProfileOutputDto>(Constants.ResultCdFail, "Please login before using this API.");
                //}
                var dao = CreateDao<UserDao>();
                return dao.GetUserProfileById(userId);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
