using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.DAO.UserDao.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class EditProfileBusinessLogic : BaseBusinessLogic
    {
        public EditProfileBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EditProfile(EditUserProfileInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<CustomerDao>();
                return dao.EditProfileById(inputDto);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
