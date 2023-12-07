using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.FoodDetail
{
    public class GetFoodByCategoryBusinessLogic : BaseBusinessLogic
    {
        public GetFoodByCategoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto GetFoodByCategory(GetFoodByCategoryDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<FoodDao>();
                return dao.GetFoodByCategory(inputDto);
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
