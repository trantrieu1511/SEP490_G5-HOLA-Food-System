using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDAO;
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
                var dao = this.CreateDao<ShopDAO>();
                DAO.UserDAO.DisplayShopOutputDto daooutput = dao.DisplayShop(inputDto);
                var output = mapper.Map<DAO.UserDAO.DisplayShopOutputDto, DisplayShopOutputDto>(daooutput);
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
