using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using System.Runtime.CompilerServices;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class UpdateStatusTransferCdBusinessLogic : BaseBusinessLogic
    {
        public UpdateStatusTransferCdBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateStatusCd(CreateTransferInputDto inputDto)
        {
            try
            {
                var now = DateTime.Now;
                var dao = this.CreateDao<TransactionDao>();
                var dao2 = this.CreateDao<UserDao>();
                var userInfor = dao2.GetUserInfo(new GetOrderInfoInputDto()
                {
                    UserId = inputDto.SenderId,
                });

                var recievierInfo = dao2.GetUserInfo(new GetOrderInfoInputDto()
                {
                    UserId  = inputDto.RecievierId,
                });
                if (recievierInfo == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "RecieverId not Exsit!");
                }

                decimal balance = userInfor.Balance.Value;
                if (balance < inputDto.Value)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Balance not enough!");
                }

                var output1 = dao.GetTransferWallet(inputDto.SenderId);
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
                    var output2 = dao.UpdateStatusUsed(output1.CodeId);
                }
                else
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Code not correct!");
                }

                // create transaction
                var transactionInput = new CreateTransaction()
                {
                    UserId = inputDto.SenderId,
                    RecieverId = inputDto.RecievierId,
                    TransactionType = 2,
                    Value = inputDto.Value,
                    Note = "Send from " + inputDto.SenderId + " to " + inputDto.RecievierId,
                    Status = 1,
                    CreateDate = now
                };

                var output = dao.Create(transactionInput);
                if (!output.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                var input3 = new UpadateWalletBalanceDaoInputDto()
                {
                    UserId = inputDto.SenderId,
                    Value = -inputDto.Value,
                };

                if (inputDto.SenderId.Contains("CU"))
                {
                    var output3 = dao.UpdateWalletBalance(input3);
                }
                else
                {
                    var output3 = dao.UpdateWalletBalanceSeller(input3);
                }

                var input4 = new UpadateWalletBalanceDaoInputDto()
                {
                    UserId = inputDto.RecievierId,
                    Value = inputDto.Value,
                };

                if (inputDto.RecievierId.Contains("CU"))
                {
                    var output3 = dao.UpdateWalletBalance(input4);
                }
                else
                {
                    var output3 = dao.UpdateWalletBalanceSeller(input4);
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
