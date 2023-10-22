using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class UpdateAmountCartItemBusinessLogic : BaseBusinessLogic
    {
        public UpdateAmountCartItemBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateItemAmount(UpdateAmoutCartItemDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CartDao>();
                return dao.UpdateAmoutCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
