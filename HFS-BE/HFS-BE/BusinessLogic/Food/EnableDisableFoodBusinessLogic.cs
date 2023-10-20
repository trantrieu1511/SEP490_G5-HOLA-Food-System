using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Food
{
    public class EnableDisableFoodBusinessLogic : BaseBusinessLogic
    {
        public EnableDisableFoodBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EnableDisableFood(FoodEnableDisableInputDto input)
        {
            try
            {
                var dao = this.CreateDao<FoodDao>();
                var output = dao.EnableDisableFood(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
