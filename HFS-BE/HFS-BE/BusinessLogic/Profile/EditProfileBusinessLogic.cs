using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.Profile
{
    public class EditProfileBusinessLogic : BaseBusinessLogic
    {
        public EditProfileBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EditProfile(EditUserProfileInputDto inputDto, string? userId)
        {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.EditProfileById(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
		public BaseOutputDto EditIdCard(EditCardInputDto inputDto, string? userId)
		{
			try
			{
				var dao = CreateDao<UserDao>();
				var fileNames1 = string.Empty;
				var fileNames2 = string.Empty;
				var daoinput = mapper.Map<EditCardInputDto, ShipperEditInputDto>(inputDto);
				daoinput.UserId = userId;
				if (inputDto.Images1 != null)
					fileNames1 = ReadSaveImage.SaveIdCardImages(inputDto.Images1, inputDto.Email, 7);
				if (inputDto.Images2 != null)
					fileNames2 = ReadSaveImage.SaveIdCardImages(inputDto.Images2, inputDto.Email, 7);

				daoinput.IdcardFrontImage = fileNames1;
				daoinput.IdcardBackImage = fileNames2;
				return dao.EditIdCard(daoinput);
			}
			catch (Exception)
			{
				return Output<UserProfileOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
