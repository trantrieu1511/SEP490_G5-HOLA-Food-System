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
        public DisplayShopOutputDto DisplayShop(DisplayShopInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<ShopDAO>();
                DAO.UserDAO.DisplayShopOutputDto daooutput = dao.DisplayShop();
                var output = mapper.Map<DAO.UserDAO.DisplayShopOutputDto, DisplayShopOutputDto>(daooutput);
                int a = (inputDto.pageNum - 1) * inputDto.pageSize;
                output.ListShop = output.ListShop.Skip(a).Take(inputDto.pageSize).ToList();
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
