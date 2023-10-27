using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ShopDetail
{
    public class GetShopInforBusinessLogic : BaseBusinessLogic
    {
        public GetShopInforBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetShopDetailDaoOutputDto GetShopInfor(GetShopDetailDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<ShopDao>();
                var output = dao.GetShopDetail(inputDto);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
