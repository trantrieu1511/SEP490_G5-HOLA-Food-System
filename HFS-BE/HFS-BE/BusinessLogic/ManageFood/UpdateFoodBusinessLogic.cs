using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.FoodImageDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.ManageFood
{
    public class UpdateFoodBusinessLogic : BaseBusinessLogic
    {
        public UpdateFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateFood(FoodUpdateInputDto inputDto)
        {
            try
            {
                /*inputDto.UserDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 2,
                    UserId = 1,
                };*/

                var daoCate = CreateDao<CategoryDao>();
                var cate = daoCate.GetCategoryById(inputDto.CategoryId);
                if (cate == null)
                {
                    var errors = new List<string>();
                    errors.Add("CategoryId: " + inputDto.CategoryId + " is not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", errors);
                }

                var foodDao = CreateDao<FoodDao>();

                //check exist food name of shop or not
                var foods = foodDao.GetAllFoodSeller(inputDto.UserDto);
                var existFoodName = foods.Foods.FirstOrDefault(x => x.Name.Replace(" ", "").ToLower().Equals(inputDto.Name.Replace(" ", "").ToLower()));

                if (foods.Foods != null &&  existFoodName != null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Update Failed", "Food name is current used!");
                }

                var output = foodDao.UpdateFood(
                        mapper.Map<FoodUpdateInputDto, FoodUpdateInforInputDto>(inputDto)
                    );
                // update fail return 
                if (!output.Success)
                    return output;
                // ** check file list iamge
                // * get list image by foodId
                var fImageDao = CreateDao<FoodImageDao>();
                var listImagesModel = fImageDao.GetAllImageByFoodId(inputDto.FoodId);

                // if img FE request && image in DB =  empty -> no change -> finish
                if ((inputDto.Images == null || inputDto.Images.Count <= 0) && listImagesModel == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                // if img FE request = empty && image in DB > 0   -> Remove -> finish
                if ((inputDto.Images == null || inputDto.Images.Count <= 0) && listImagesModel != null)
                {
                    foreach (var img in listImagesModel)
                    {
                        img.FoodId = null;
                        fImageDao.UpdateImageInfor(img);
                    }
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                // if img FE request > 0 && image in DB < 0   -> Add new -> finish
                if ((inputDto.Images != null && inputDto.Images.Count > 0) && listImagesModel == null)
                {
                    SaveAndAddImage(inputDto.Images, inputDto.UserDto, inputDto.FoodId);
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }

                if(inputDto.Images != null && listImagesModel != null)
                {
                    // * check what image
                    // if: new (Add new)
                    // else if: remove (update foodId -> NULL)
                    // else: old(don't care)

                    // -- remove (update foodId -> NULL)
                    var removeImages = listImagesModel.Where(img =>
                        // check image in DB (1) vs img FE request (2)
                        // if (1) not in (2) => true
                        inputDto.Images.FirstOrDefault(i => i.FileName.Equals(img.Path)) == null ? true : false
                    ).ToList();
                    // - update foodId -> NULL
                    foreach (var img in removeImages)
                    {
                        img.FoodId = null;
                        fImageDao.UpdateImageInfor(img);
                    }

                    // -- new (Add new)
                    var newImages = inputDto.Images.Where(img =>
                       // check  img FE request (1) vs image in DB (2)
                       // if (1) not in (2) => true
                       listImagesModel.FirstOrDefault(i => i.Path.Equals(img.FileName)) == null ? true : false
                    ).ToList();

                    // - Save to image and Add new FoodImg
                    SaveAndAddImage(newImages, inputDto.UserDto, inputDto.FoodId);
                }

                
                /*List<string> fileNames = ReadSaveImage.SaveImages(newImages, inputDto.UserDto, 1);
                // - 
                foreach (var img in fileNames)
                {
                    var foodImgModel = new FoodImage
                    {
                        FoodId = inputDto.FoodId,
                        Path = img
                    };

                    fImageDao.AddNewImage(foodImgModel);
                }*/



                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        private void SaveAndAddImage(IReadOnlyList<IFormFile> newImages, UserDto user, int foodId)
        {
            var fImageDao = CreateDao<FoodImageDao>();
            // - Save to image
            List<string> fileNames = ReadSaveImage.SaveImages(newImages, user, 1);
            // - Add new FoodImg
            foreach (var img in fileNames)
            {
                var foodImgModel = new FoodImage
                {
                    FoodId = foodId,
                    Path = img
                };

                fImageDao.AddNewImage(foodImgModel);
            }
        }
    }
}
