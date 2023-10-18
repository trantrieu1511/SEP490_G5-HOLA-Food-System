using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class DisplayProfileBusinessLogic : BaseBusinessLogic
    {
        public DisplayProfileBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public UserProfileOutputDto GetProfile(GetUserProfileInputDto inputDto) {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.GetUserProfileById(inputDto);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
