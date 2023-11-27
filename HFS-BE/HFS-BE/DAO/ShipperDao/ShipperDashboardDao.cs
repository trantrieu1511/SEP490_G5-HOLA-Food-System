using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.ShipperDao
{
	public class ShipperDashboardDao : BaseDao
	{
		public ShipperDashboardDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		public List<DashBoardShipperOutputDto> DashBoardShipper(DashBoardShipperInputDto input) {

			List<DashBoardShipperOutputDto> outputDto = new List<DashBoardShipperOutputDto>();
			foreach (var date in input.dates)
			{
				var data = context.OrderProgresses.Where(s => s.CreateDate.Value.Date == date.Date &&s.ShipperId==input.ShipperId&&s.Status==5).Count();
				if (data != null)
				{
					DashBoardShipperOutputDto output = new DashBoardShipperOutputDto();
					output.Status = 5;
					output.CreateDate = date;
					output.Data = data;
					outputDto.Add(output);
				}
			
				var data2 = context.OrderProgresses.Where(s => s.CreateDate.Value.Date == date.Date && s.ShipperId == input.ShipperId && s.Status == 4).Count();
				if (data2 != null)
				{
					DashBoardShipperOutputDto output = new DashBoardShipperOutputDto();
					output.Status = 4;
					output.CreateDate = date;
					output.Data = data;
					outputDto.Add(output);
				}
				
			}
		


			
			return outputDto;
		}

		public DashBoardShipperTotalOutputDto DashBoardShipperTotal(DashBoardShipperTotalInputDto input)
		{
			DashBoardShipperTotalOutputDto outputDto = new DashBoardShipperTotalOutputDto();
			var data = context.OrderProgresses.Where(s => s.ShipperId == input.ShipperId && s.Status == 5).Count();
			var data2 = context.OrderProgresses.Where(s =>  s.ShipperId == input.ShipperId && s.Status == 4).Count();
			var data3 = context.OrderProgresses.Where(s => s.CreateDate.Value.Date == DateTime.Now.Date && s.ShipperId == input.ShipperId && s.Status == 4).Count();
			var data4 = context.OrderProgresses.Where(s => s.CreateDate.Value.Date == DateTime.Now.Date && s.ShipperId == input.ShipperId && s.Status == 5).Count();
			var data5 = context.OrderProgresses.Where(s => s.CreateDate.Value.Month == DateTime.Now.Month && s.CreateDate.Value.Year==DateTime.Now.Year
		 && s.ShipperId == input.ShipperId && s.Status == 4).Count();
			var data6 = context.OrderProgresses.Where(s => s.CreateDate.Value.Month == DateTime.Now.Month && s.CreateDate.Value.Year == DateTime.Now.Year
			&& s.ShipperId == input.ShipperId && s.Status == 5).Count();

			var data7 = context.Orders
				.Include(s=>s.OrderProgresses)
				.Include(s => s.OrderDetails)
				.Where(s => s.ShipperId==input.ShipperId&&s.ShippedDate.Value.Date==DateTime.Now.Date).ToList();
			decimal sumday = 0; 
			foreach (var s in data7)
			{
				foreach (var o in s.OrderDetails)
				{
					sumday = (decimal)o.Quantity * (decimal)o.UnitPrice;
				}
				if (s.VoucherId !=null)
				{
					sumday = sumday - s.Voucher.DiscountAmount;
				}
		
			}
			var data8 = context.Orders
				.Include(s => s.OrderProgresses)
				.Include(s => s.OrderDetails)
				.Where(s => s.ShipperId == input.ShipperId && s.ShippedDate.Value.Month == DateTime.Now.Month).ToList();
			decimal summonth = 0;
			foreach (var s in data8)
			{
				foreach (var o in s.OrderDetails)
				{
					summonth = (decimal)o.Quantity * (decimal)o.UnitPrice;
				}
				if (s.VoucherId != null)
				{
					summonth = sumday - s.Voucher.DiscountAmount;
				}

			}
			
			var sum = data + data2;
			outputDto.Total = sum;
			outputDto.OrderDayCom = data3;
			outputDto.OrderDayInCom = data4;
			outputDto.OrderDay = data3+data4;
			outputDto.OrderMothCom = data5;
			outputDto.OrderMothInCom = data6;
			outputDto.OrderMonth = data5 + data6;
			outputDto.AmoutMonth = summonth;
			outputDto.AmoutDay = sumday;
			return outputDto;
		}
	}
}
