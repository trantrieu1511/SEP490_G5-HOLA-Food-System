using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.ShipperDao
{
    public class ShipperDao : BaseDao
    {
        public ShipperDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ShipperInforList GetShippersBySellerId(string sellerId)
        {
            // get shipper of shop
            // with condition: not ban and verified
            var data = context.Shippers
                .Where(s => s.ManageBy.Equals(sellerId) && s.IsVerified == true && s.IsBanned == false)
                .Select(s => mapper.Map<Shipper, ShipperInfor>(s))
                .ToList();
            var output = Output<ShipperInforList>(Constants.ResultCdSuccess);
            output.Shippers = data;
            return output;
        }

        public Shipper? GetShipperByShipperIdAndSellerId(string sellerId, string shipperId)
        {
            return context.Shippers.FirstOrDefault(x => x.ShipperId.Equals(shipperId) && x.ManageBy.Equals(sellerId));
        }

        public ShipperInforList GetShipperAll()
		{
			var data = context.Shippers
				.Select(s => mapper.Map<Shipper, ShipperInfor>(s))
				.ToList();
			var output = Output<ShipperInforList>(Constants.ResultCdSuccess);
			output.Shippers = data;
			return output;
		}

	}
}
