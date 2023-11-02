using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageVoucher
{
    public class GetVoucherBusinessLogic : BaseBusinessLogic
    {
        public GetVoucherBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public GetListVoucherDaoOutputDto GetListVoucher(GetListVoucherDaoInput inputDto)
        {
            try
            {
                var dao = this.CreateDao<VoucherDao>();
                var output = dao.GetListVoucher(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
