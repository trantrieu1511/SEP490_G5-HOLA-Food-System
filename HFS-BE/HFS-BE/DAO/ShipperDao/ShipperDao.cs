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

        public ShipperInforList GetShipperNoneManager()
        {
            var data = context.Shippers.Where(s => s.ManageBy == null)
                .Select(s => mapper.Map<Shipper, ShipperInfor>(s))
                .ToList();
            var output = Output<ShipperInforList>(Constants.ResultCdSuccess);
            output.Shippers = data;
            return output;
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
