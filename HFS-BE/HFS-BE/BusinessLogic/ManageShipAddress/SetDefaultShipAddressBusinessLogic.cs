using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageShipAddress
{
    public class SetDefaultShipAddressBusinessLogic : BaseBusinessLogic
    {
        public SetDefaultShipAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto SetDefaultShipAddress(SetDefaultShipAddressInputDto inputDto, string userId)
        {
            try
            {
                var dao = CreateDao<ShipAddressDao>();
                return dao.SetDefaultShipAddress(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
