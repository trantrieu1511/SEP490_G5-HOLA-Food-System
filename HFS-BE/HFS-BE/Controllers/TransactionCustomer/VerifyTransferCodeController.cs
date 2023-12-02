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
    public class VerifyTransferCodeController : BaseController
    {
        public VerifyTransferCodeController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/verifycode")]
        [Authorize]
        public BaseOutputDto VerifyCode(CreateTransferInputDto inputDto)
        {
            try
            {
                var user = this.GetUserInfor();
                var busi = this.GetBusinessLogic<UpdateStatusTransferCdBusinessLogic>();
                inputDto.SenderId = user.UserId;
                inputDto.TransactionType = 2;
                return busi.UpdateStatusCd(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
