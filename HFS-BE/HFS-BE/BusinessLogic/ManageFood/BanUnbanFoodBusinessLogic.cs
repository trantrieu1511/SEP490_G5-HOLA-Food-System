using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageFood
{
    public class BanUnbanFoodBusinessLogic : BaseBusinessLogic
    {
        public BanUnbanFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto BanUnbanFood(FoodBanUnbanInputDto inputDto, string userId) {
            try
            {
                var dao = CreateDao<FoodDao>();
                return dao.BanUnbanFood(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
