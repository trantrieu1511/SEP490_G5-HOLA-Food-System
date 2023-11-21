using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Search
{
    public class SearchShopBusinessLogic : BaseBusinessLogic
    {
        public SearchShopBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DisplayShopDaoOutputDto SearchShop(SearchShopInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<ShopDao>();
                return dao.DisplayShopByName(inputDto);
            }
            catch (Exception)
            {
                return this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
