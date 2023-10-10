using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class DisplayShopBusinessLogic : BaseBusinessLogic
    {
        public DisplayShopBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DisplayShopOutputDto DisplayShop(BaseInputDto inputDto)
        {
            try
            {
                var Dao = this.CreateDao<ShopDAO>();
                Dao.ShopDao.DisplayShopOutputDto daooutput = Dao.DisplayShop(inputDto);
                var output = mapper.Map<Dao.ShopDao.DisplayShopOutputDto, DisplayShopOutputDto>(daooutput);
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
