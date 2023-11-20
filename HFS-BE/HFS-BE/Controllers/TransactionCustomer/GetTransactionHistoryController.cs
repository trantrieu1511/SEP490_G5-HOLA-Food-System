using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.TransactionCustomer
{
    public class GetTransactionHistoryController : BaseController
    {
        public GetTransactionHistoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/history")]
        [Authorize]
        public GetTransactionHistoryDaoOutputDto GetTransaction(GetTransactionHistoryDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                var busi = this.GetBusinessLogic<GetTransactionHistoryBusinessLogic>();
                inputDto.UserId = userInfo.UserId;
                return busi.GetTransactionHistory(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
