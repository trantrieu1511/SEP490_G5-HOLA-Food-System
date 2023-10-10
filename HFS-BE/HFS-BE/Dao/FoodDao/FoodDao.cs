using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;


namespace HFS_BE.Dao.FoodDao
{
    public class FoodDao : BaseDao
    {
        public FoodDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public DisplayFoodOutputDto AllFood()
        {
            return null;
        }
    }
}
