using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class GetHotFoodBusinessLogic : BaseBusinessLogic
    {
        public GetHotFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto GetHotFoods()
        {
            try
            {
                var dao = this.CreateDao<FoodDao>();
                return dao.GetHotFood();
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
