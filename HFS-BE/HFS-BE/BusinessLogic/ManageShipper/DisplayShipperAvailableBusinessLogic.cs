using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageShipper
{
    public class DisplayShipperAvailableBusinessLogic : BaseBusinessLogic
    {
        public DisplayShipperAvailableBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ShipperInforList DisplayShipperAvailable(ShipperBySellerInputDto input)
        {
            try
            {
                var dao = CreateDao<ShipperDao>();
                return dao.GetShippersBySellerId(input.User.UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
