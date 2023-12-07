using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class GetShipperInforBusinessLogic : BaseBusinessLogic
    {
        public GetShipperInforBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetShipperInforOutputDto GetShipper(string shipperId)
        {
            try
            {
                var dao = this.CreateDao<ShipperDao>();
                return dao.GetShipperInfor(shipperId);
            }
            catch (Exception)
            {
                return this.Output<GetShipperInforOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
