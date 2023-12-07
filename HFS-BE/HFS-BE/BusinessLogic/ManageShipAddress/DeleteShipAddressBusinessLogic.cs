using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageShipAddress
{
    public class DeleteShipAddressBusinessLogic : BaseBusinessLogic
    {
        public DeleteShipAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto DeleteShipAddress(DeleteShipAddressInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<ShipAddressDao>();
                return dao.DeleteShipAddress(inputDto);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
