using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageShipAddress
{
    public class UpdateShipAddressBusinessLogic : BaseBusinessLogic
    {
        public UpdateShipAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateShipAddress(UpdateShipAddressInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<ShipAddressDao>();
                return dao.UpdateShipAddress(inputDto);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
