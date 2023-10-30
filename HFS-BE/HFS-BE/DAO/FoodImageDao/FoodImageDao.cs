using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.FoodImageDao
{
    public class FoodImageDao : BaseDao
    {
        public FoodImageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<FoodImage>? GetAllImageByFoodId(int foodId)
        {
            var output = context.FoodImages.Where(
                    i => i.FoodId == foodId
                ).ToList();
            if(output.Count > 0)
                return  output ;
            return null;
        }

        public BaseOutputDto UpdateImageInfor(FoodImage image)
        {
            try
            {
                var imageModel = context.FoodImages.FirstOrDefault(
                    img => img.ImageId == image.ImageId
                );

                imageModel.Path = image.Path;
                imageModel.FoodId = image.FoodId;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddNewImage(FoodImage image)
        {
            try
            {
                context.Add(image);
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}
