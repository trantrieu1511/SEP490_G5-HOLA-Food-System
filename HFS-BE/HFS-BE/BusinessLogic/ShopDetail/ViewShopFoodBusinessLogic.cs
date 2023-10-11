using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Ultis;

namespace HFS_BE.BusinessLogic.Menu
{
    public class ViewShopFoodBusinessLogic : BaseBusinessLogic
    {
        public ViewShopFoodBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto FoodShop(FoodByShopDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<FoodDao>();
                return dao.FoodByShop(inputDto);
            }
            catch (Exception)
            {

                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
