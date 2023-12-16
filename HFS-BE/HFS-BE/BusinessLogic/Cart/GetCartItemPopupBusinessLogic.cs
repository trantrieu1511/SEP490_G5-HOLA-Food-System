using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class GetCartItemPopupBusinessLogic : BaseBusinessLogic
    {
        public GetCartItemPopupBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetCartItemDaoOutputDto GetCartItem(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CartDao>();
                return dao.GetCartItemPopup(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdSuccess);
            }
        }
    }
}
