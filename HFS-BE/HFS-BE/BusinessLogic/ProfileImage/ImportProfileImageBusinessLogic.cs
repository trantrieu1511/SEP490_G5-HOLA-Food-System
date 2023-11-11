using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.ProfileImage;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ProfileImage
{
    public class ImportProfileImageBusinessLogic : BaseBusinessLogic
    {
        public ImportProfileImageBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ImportProfileImage(ProfileImageInputDto inputDto)
        {
            try
            {
                if (inputDto.UserId == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login before using this API.");
                }
                var dao = CreateDao<ProfileImageDao>();
                var inputMapper = mapper.Map<ProfileImageInputDto, DAO.ProfileImage.ProfileImageInputDto>(inputDto);

                // save the file to server -> return the file name
                var fileName = "";
                fileName = ReadSaveImage.SaveProfileImage(inputDto.ImageName, inputDto.UserId);
                inputMapper.ImageName = fileName;

                var output = dao.ImportProfileImage(inputMapper);

                return output;
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
