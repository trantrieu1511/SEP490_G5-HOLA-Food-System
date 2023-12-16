using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using System.Reflection.Metadata;

namespace HFS_BE.BusinessLogic.Search
{
    public class SearchBusinessLogic : BaseBusinessLogic
    {
        public SearchBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto Search(FoodByNameDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<FoodDao>();
                return dao.FoodByName(inputDto);
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
