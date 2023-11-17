using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageShipAddress
{
    public class GetAllShipAddressBusinessLogic : BaseBusinessLogic
    {
        public GetAllShipAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListShipAddressOutputDto GetAllShipAddress(string userId)
        {
            try
            {
                var dao = CreateDao<ShipAddressDao>();
                return dao.GetAllShipAddressByUserId(userId);
            }
            catch (Exception)
            {
                return Output<ListShipAddressOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
