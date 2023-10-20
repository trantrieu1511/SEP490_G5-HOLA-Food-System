using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.Food
{
    public class AddNewFoodBusinessLogic : BaseBusinessLogic 
    {
        public AddNewFoodBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewFood(FoodCreateInputDto inputDto)
        {
            try
            {
                inputDto.UserDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 2,
                    UserId = 1,
                };
                var fileNames = new List<string>();
                // save file to server -> return list file name
                if (inputDto.Images != null && inputDto.Images.Count > 0)
                    fileNames = ReadSaveImage.SaveImages(inputDto.Images, inputDto.UserDto, 1);

                var Dao = this.CreateDao<FoodDao>();
                var inputMapper = mapper.Map<FoodCreateInputDto, Dao.FoodDao.FoodCreateInputDto>(inputDto);
                inputMapper.Images = fileNames;

                var output = Dao.AddNewFood(inputMapper);
                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
