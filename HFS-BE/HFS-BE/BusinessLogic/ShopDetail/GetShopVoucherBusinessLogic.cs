using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ShopDetail
{
    public class GetShopVoucherBusinessLogic : BaseBusinessLogic
    {
        public GetShopVoucherBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetListVoucherDaoOutputDto GetListVoucher(GetListVoucherDaoInput inputDto)
        {
            try
            {
                var dao = this.CreateDao<VoucherDao>();
                var output = dao.GetShopVoucher(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
