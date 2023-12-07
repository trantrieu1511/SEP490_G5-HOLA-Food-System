using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageVoucher
{
    public class EnableDisableBusinessLogic : BaseBusinessLogic
    {
        public EnableDisableBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto EnableDisableVoucher(Enable_Disable_VoucherDaoInput input)
        {
            try
            {
                var dao = this.CreateDao<VoucherDao>();
                var voucher = dao.GetVoucherById(input.VoucherId);
                var errors = new List<string>();
                if (voucher == null)
                {
                    errors.Add($"VoucherId: {input.VoucherId} not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    //return Output<BaseOutputDto>(Constants.ResultCdFail, $"PostId: {input.PostId} not exist!");
                }
                // check status Ban 
                var output = dao.Enable_Disable_Voucher(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}
