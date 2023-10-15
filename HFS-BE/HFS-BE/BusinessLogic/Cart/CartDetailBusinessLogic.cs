using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CartDetailBusinessLogic : BaseBusinessLogic
    {
        public CartDetailBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public CartDetailBusinessLogicOutputDto GetCartDetail(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CartDao>();
                var daoOutput = dao.GetCartItem(inputDto);
                var output = this.Output<CartDetailBusinessLogicOutputDto>(Constants.ResultCdSuccess);

                foreach (var item in daoOutput.ListItem)
                {
                    if (!output.ListShop.Select(x => x.ShopId).Contains(item.ShopId))
                    {
                        List<CartItemOutputDto> listitem = daoOutput.ListItem.Where(x => x.ShopId == item.ShopId).ToList();
                        ListShopItemDto shopitem = new ListShopItemDto()
                        {
                            ShopId = item.ShopId,
                            ShopName = item.ShopName,
                            ListItem = mapper.Map<List<CartItemOutputDto>, List<CartItemDto>>(listitem),
                        };
                    }
                }

                return output;

            }
            catch (Exception)
            {
                return this.Output<CartDetailBusinessLogicOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
