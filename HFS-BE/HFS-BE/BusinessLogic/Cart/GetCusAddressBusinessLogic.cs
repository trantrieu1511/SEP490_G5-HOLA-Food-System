using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class GetCusAddressBusinessLogic : BaseBusinessLogic
    {
        public GetCusAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetUserAddressDaoOutputDto GetAddresss(GetAddressInfoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<UserDao>();
                return dao.GetCusAddress(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetUserAddressDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
