using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.TransactionWallet
{
    public class UpdateWithdrawStatusAccController : BaseController
    {
        public UpdateWithdrawStatusAccController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/updatewithdrawstatus")]
        [Authorize]
        public BaseOutputDto UpdateStatusWithdrawRequest(UpdateTransationStatusDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (!userInfo.Role.Equals("AC"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Unauthorized");
                }

                var busi = this.GetBusinessLogic<UpdateStatusWithdrawRequestBusinessLogic>();
                inputDto.AccountantId = userInfo.UserId;
                return busi.UpdateStatusWithDrawRequest(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
