 using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageUser.ManageShipper
{
	public class ShipperBusinessLogic : BaseBusinessLogic
	{
		public ShipperBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ShipperInforList ListShipper()
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.GetShipperAll();

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
