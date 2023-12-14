using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManageFood
{
    public class GetFoodBusinessLogic : BaseBusinessLogic
    {
        public GetFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }


        public ListFoodOutputSellerDto ListFoods(UserDto userDto)
        {
            try
            {
                /*userDto = new Utils.UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 1,
                    UserId = 1,
                };*/

                var dao = this.CreateDao<FoodDao>();

                // Divide into 2 cases: Seller, menu moderator
                string? userRole = userDto.UserId.Substring(0, 2);
                Dao.FoodDao.ListFoodOutputSellerDto daoOutput = null;
                switch (userRole)
                {
                    case "SE":
                        daoOutput = dao.GetAllFoodSeller(userDto);
                        break;
                    case "MM":
                        daoOutput = dao.GetAllFoodMenuModerator();
                        break;
                }

                var outputBL = mapper.Map<Dao.FoodDao.ListFoodOutputSellerDto, ListFoodOutputSellerDto>(daoOutput);

                foreach (var food in daoOutput.Foods)
                {
                    // get current index
                    var index = daoOutput.Foods.IndexOf(food);

                    if (food.Images == null || food.Images.Count < 1)
                    {
                        continue;
                    }

                    foreach (var img in food.Images)
                    {
                        ImageFileConvert.ImageOutputDto? imageInfor = null;
                        // convert to base64 with 2 cases
                        switch (userRole)
                        {
                            case "SE":
                                imageInfor = ImageFileConvert.ConvertFileToBase64(userDto.UserId, img.Path, 1);
                                break;
                            case "MM":
                                imageInfor = ImageFileConvert.ConvertFileToBase64(food.SellerId, img.Path, 1);
                                break;
                        }
                        if (imageInfor == null)
                            continue;
                        var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, FoodImageOutputSellerDto>(imageInfor);
                        imageMapper.ImageId = img.ImageId;

                        // add to ouput list
                        outputBL.Foods[index].ImagesBase64.Add(imageMapper);
                    }

                    // calculate rating
                    var rating = RatingCalculation.CalculateFoodStar(food.Feedbacks);
                    // add rating to ouput list
                    outputBL.Foods[index].Rating = rating;
                }

                return outputBL;
            }
            catch (Exception)
            {
                return this.Output<ListFoodOutputSellerDto>(Constants.ResultCdFail);
            }
        }

    }
}
