using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageVoucher
{
    public class AddNewVoucherBusinessLogic : BaseBusinessLogic
    {
        public AddNewVoucherBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto AddNewVoucher(CreateVoucherDaoInputDto inputDto)
        {
            try
            {
                var daoVou = CreateDao<VoucherDao>();
                var output = daoVou.CreateVoucher(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<CreateVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
            
        }
    }
}
