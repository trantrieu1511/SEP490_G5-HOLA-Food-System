using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using static HFS_BE.Utils.Enum.TransactionTypeEnum;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class CreateWithdrawRequestBusinessLogic : BaseBusinessLogic
    {
        public CreateWithdrawRequestBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateWithdrawRequest(CreateWithdrawInputDto inputDto)
        {
            try
            {
                var now = DateTime.Now;
                var dao = this.CreateDao<TransactionDao>();
                var dao2 = this.CreateDao<UserDao>();
                var userInfor = dao2.GetUserInfo(new GetOrderInfoInputDto()
                {
                    UserId = inputDto.UserId,
                });

                if (userInfor.Balance < inputDto.Value)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Balance not enough!");
                }

                var output1 = dao.GetTransferWallet(inputDto.UserId);
                if (output1 == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Code not exsit!");
                }

                if (output1.ExpiredDate < now)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Code expired!");
                }

                if (output1.IsUsed.Value)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Code Used, send again!");
                }

                if (output1.Code.Equals(inputDto.Code))
                {
                    var output3 = dao.UpdateStatusUsed(output1.CodeId);
                }
                else
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Code not correct!");
                }

                var input = new CreateTransaction()
                {
                    UserId = inputDto.UserId,
                    TransactionType = 1,
                    Status = 0,
                    Value = inputDto.Value,
                    Note = inputDto.Note,
                    CreateDate = now,
                    ExpiredDate = now.AddDays(7),
                };

                var output = dao.Create(input);
                if (!output.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "System Error!");
                }

                var input2 = new UpadateWalletBalanceDaoInputDto()
                {
                    UserId = inputDto.UserId,
                    Value = -inputDto.Value,
                };
                var output2 = dao.UpdateWalletBalanceSeller(input2);
                if (!output2.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "System Error!");
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
