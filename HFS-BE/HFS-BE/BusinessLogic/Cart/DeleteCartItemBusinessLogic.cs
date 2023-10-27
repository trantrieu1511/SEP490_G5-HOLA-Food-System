using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class DeleteCartItemBusinessLogic : BaseBusinessLogic
    {
        public DeleteCartItemBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto DeleteCartItem(DeleteCartItemInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CartDao>();
                return dao.DeleteCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
