 using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;
using System.Buffers.Text;

namespace HFS_BE.BusinessLogic.ManageUser.ManageShipper
{
	public class ShipperBusinessLogic : BaseBusinessLogic
	{
		public ShipperBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListShipperbyAdminOutputDtoBS ListShipperbyAdmin()
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.GetShipperAll();
				var outputBL = mapper.Map<ShipperInforListByAdmin, ListShipperbyAdminOutputDtoBS>(daooutput);//chua map
				foreach (var ship in daooutput.Shippers)
				{
					// get current index
					var index = daooutput.Shippers.IndexOf(ship);

					if (ship.Images == null || ship.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in ship.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(ship.ShipperId, img.Path, 3);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, ShipperImageOutputDto>(imageInfor);//chưa map
						imageMapper.ImageId = img.ImageId;

						// add to ouput list
						outputBL.Shippers[index].ImagesBase64.Add(imageMapper);
					}
				}
				return outputBL;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ShipperInforList ListShipperBySeller(ShipperInforDtoInputbySellerId input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.GetShippersBySellerId(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListInvitationShipperDtoOutput ListInvitationShipper(ShipperInforDtoInputbySellerId input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.ListInvitationShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListInvitationShipperbyShipperDtoOutput ListInvitationShipperbyShipper(ListInvitationShipperDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.ListInvitationShipperByShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto InvitationShipper(InvitationShipperEmailDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.InvitationShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public BaseOutputDto BanShipper(BanShipperDtoInput input)
		//{
		//	try
		//	{
		//		var Dao = this.CreateDao<ShipperDao>();
		//		var daooutput = Dao.BanShipper(input);

		//		return daooutput;
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}
		public BaseOutputDto AcceptShipper(InvitationShipperDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.AcceptInvitationShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto ActiveShipper(ActiveShipperDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.ActiveShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto KickShipper(KickShipperDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDao>();
				var daooutput = Dao.KickShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		//public ListHistoryBanShipper ListHistoryBanShipper(BanShipperHistoryDtoInput input)
		//{
		//	try
		//	{
		//		var Dao = this.CreateDao<ShipperDao>();

		//		var daooutput = Dao.ListHistoryShipper(input);

		//		return daooutput;
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}
		public List<DashBoardShipperOutputDto> DashBoardShipper(DashBoardShipperInputDto input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDashboardDao>();

				var daooutput = Dao.DashBoardShipper(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public DashBoardShipperTotalOutputDto DashBoardShipperTotal(DashBoardShipperTotalInputDto input)
		{
			try
			{
				var Dao = this.CreateDao<ShipperDashboardDao>();

				var daooutput = Dao.DashBoardShipperTotal(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
