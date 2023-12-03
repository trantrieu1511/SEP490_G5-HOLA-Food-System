using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.TransactionWallet
{
    public class CreateWithdrawRequestController : BaseController
    {
        public CreateWithdrawRequestController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/withdraw")]
        [Authorize]
        public BaseOutputDto CreateWithdrawRequest(CreateWithdrawInputDto inputDto)
        {
            try
            {
                var userInfor = this.GetUserInfor();
                if (!userInfor.Role.Equals("SE"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Unauthorized");
                }

                var busi = this.GetBusinessLogic<CreateWithdrawRequestBusinessLogic>();
                inputDto.UserId = userInfor.UserId;
                var output = busi.CreateWithdrawRequest(inputDto);
               return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
