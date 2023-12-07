using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class ChangeUserAccPasswordBusinessLogic : BaseBusinessLogic
    {
        public ChangeUserAccPasswordBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ChangeUserAccountPassword(ChangeUserAccountPasswordInputDto inputDto, string userId)
        {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.ChangeUserAccountPassword(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
