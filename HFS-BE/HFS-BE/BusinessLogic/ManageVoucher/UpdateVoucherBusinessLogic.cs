using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageVoucher
{
    public class UpdateVoucherBusinessLogic : BaseBusinessLogic
    {
        public UpdateVoucherBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto UpdateVoucher(UpdateVoucherDaoInput inputDto)
        {
            try
            {
                var daoVou = CreateDao<VoucherDao>();
                var output = daoVou.UpdateVoucher(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
