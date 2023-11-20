using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.TransactionCustomer
{
    public class GetUserWalletBalanceController : BaseController
    {
        public GetUserWalletBalanceController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/getbalance")]
        [Authorize]
        public GetOrderInfoOutputDto GetUserBalance()
        {
            try
            {
                var inputDto = new GetOrderInfoInputDto();
                var userInfo = this.GetUserInfor();
                inputDto.UserId = userInfo.UserId;
                var busi = this.GetBusinessLogic<GetUserBalanceBusinessLogic>();
                return busi.GetUserBalance(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetOrderInfoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
