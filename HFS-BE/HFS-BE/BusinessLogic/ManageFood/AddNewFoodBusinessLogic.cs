using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManageFood

{
    public class AddNewFoodBusinessLogic : BaseBusinessLogic 
    {
        public AddNewFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewFood(FoodCreateInputDto inputDto)
        {
            try
            {
                var daoCate = CreateDao<CategoryDao>();
                var foodDao = this.CreateDao<FoodDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var moderatorDao = CreateDao<ModeratorDao>();

                var cate = daoCate.GetCategoryById(inputDto.CategoryId);
                if (cate == null)
                {
                    var errors = new List<string>();
                    errors.Add("CategoryId: " + inputDto.CategoryId + " is not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed" , errors);
                }

                //check exist food name of shop or not
                var foods = foodDao.GetAllFoodSeller(inputDto.UserDto);
                var existFoodName = foods.Foods.FirstOrDefault(x => x.Name.Replace(" ", "").ToLower().Equals(inputDto.Name.Replace(" ", "").ToLower()));

                if (foods.Foods != null && existFoodName != null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Food name is current used!");
                }

                /*inputDto.UserDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 2,
                    UserId = 1,
                };*/
                var fileNames = new List<string>();
                // save file to server -> return list file name
                if (inputDto.Images != null && inputDto.Images.Count > 0)
                    fileNames = ReadSaveImage.SaveImages(inputDto.Images, inputDto.UserDto, 1);

                var inputMapper = mapper.Map<FoodCreateInputDto, Dao.FoodDao.FoodCreateInputDto>(inputDto);
                inputMapper.Images = fileNames;

                var output = foodDao.AddNewFood(inputMapper);
                if (!output.Success)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Failed");

                // add notify
                // 1 get all food moderator
                var moderList = moderatorDao.GetAllMenuModerator();
                foreach(var moder in moderList.data)
                {
                    // 2. gen title and content notification
                    var notify = GenerateNotification.GetSingleton().GenNotificationAddNewFood(moder.ModId, output.FoodId);
                    //3. add notify
                    var noti = notifyDao.AddNewNotification(notify);
                    if (!noti.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
