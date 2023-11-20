using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class GetUserBalanceBusinessLogic : BaseBusinessLogic
    {
        public GetUserBalanceBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetOrderInfoOutputDto GetUserBalance(GetOrderInfoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<UserDao>();
                return dao.GetUserInfo(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetOrderInfoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
