using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class AddCartItemBusinessLogic : BaseBusinessLogic
    {
        public AddCartItemBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddCartItem(AddCartItemInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CartDao>();
                return dao.AddCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
