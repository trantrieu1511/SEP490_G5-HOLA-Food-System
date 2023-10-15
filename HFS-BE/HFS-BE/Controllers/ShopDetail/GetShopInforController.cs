using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ShopDetail;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ShopDetail
{
    public class GetShopInforController : BaseController
    {
        public GetShopInforController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("shopdetail")]
        public GetShopDetailDaoOutputDto GetShopInfor(GetShopDetailDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<GetShopInforBusinessLogic>();
                return busi.GetShopInfor(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
