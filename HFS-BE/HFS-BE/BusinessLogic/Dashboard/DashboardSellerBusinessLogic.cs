using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Dashboard;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using System.Runtime.InteropServices;

namespace HFS_BE.BusinessLogic.Dashboard
{
    public class DashboardSellerBusinessLogic : BaseBusinessLogic
    {
        public DashboardSellerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DashboardSellerOutput DashboardSeller(DashboardInputDaoDto inputDto)
        {
            try
            {
                var orderDao = CreateDao<OrderDao>();

                var beforeRangeDate = new DateTime();
                var orderList = orderDao.DashboardSeller(inputDto, out beforeRangeDate);

                if (!orderList.Success)
                {
                    return Output<DashboardSellerOutput>(Constants.ResultCdFail);
                }

                var output = new DashboardSellerOutput();

                /*
                 * 0. get count and percent count follow range Date input
                 * 1. datefrom == dateEnd -> ko can lay labels va dataSets
                    return 
                 * 2. datefrom != dateEnd -> get labels and dataSets
                */

                // 0. get count and percent count follow range Date input
                // cal count in range
                var (orderCount, soldCount, amountCount) = CountInRange(orderList.Orders.Where(
                        x => x.OrderDate.Value.Date >= inputDto.DateFrom.Value.Date && 
                        x.OrderDate.Value.Date <= inputDto.DateEnd.Value.Date
                    ).ToList());
                //set count
                output.OrderCount = orderCount;
                output.AmountCount = amountCount;
                output.SoldCount = soldCount;

                // cal count before range

                var (beforeOrderCount, beforeSoldCount, beforeAmountCount) = CountBeforeRange(orderList.Orders.Where(
                        x => x.OrderDate.Value.Date >= beforeRangeDate.Date && 
                        x.OrderDate.Value.Date < inputDto.DateFrom.Value.Date
                    ).ToList());
                // cal % inrange vs before range
                // (inRange - beforeRange) / beforeRange * 100
                output.OrderCountPercent = CalculatePercentage(orderCount, beforeOrderCount);
                output.AmountCountPercent = CalculatePercentage(amountCount, beforeAmountCount);
                output.SoldCountPercent = CalculatePercentage(soldCount, beforeSoldCount);

                // 1. datefrom == dateEnd
                if (inputDto.DateFrom.Equals(inputDto.DateEnd))
                {
                    return output;
                }

                //2.datefrom != dateEnd->get labels and dataSets

                output.Labels = CalculateLabels((DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);
                output.Datasets = CalculateDataSets(orderList.Orders.Where(
                        x => x.OrderDate.Value.Date >= inputDto.DateFrom.Value.Date &&
                        x.OrderDate.Value.Date <= inputDto.DateEnd.Value.Date
                    ).ToList(), (DateTime)inputDto.DateFrom, (DateTime)inputDto.DateEnd);

                return output;

            }
            catch (Exception)
            {

                throw;
            }

            (int orderCount, int soldCount, decimal amountCount) CountInRange(ICollection<Order> orders)
            {
                var output = (orderCount: 0, soldCount: 0, amountCount: 0m);
                // order_count
                output.orderCount = orders.Count;

                foreach (var order in orders)
                {
                    //amount_count
                    output.amountCount += order.OrderDetails.Select(
                            d => d.UnitPrice ?? 0m * d.Quantity ?? 0
                        ).ToList().Sum() - (order.Voucher?.DiscountAmount ?? 0m);
                    //sold_count 
                    output.soldCount += order.OrderDetails.Count;
                }

                return output;
            }

            (int orderCount, int soldCount, decimal amountCount) CountBeforeRange(ICollection<Order> orders)
            {
                var output = (orderCount: 0, soldCount: 0, amountCount: 0m);
                // order_count
                output.orderCount = orders.Count;

                foreach (var order in orders)
                {
                    //amount_count
                    output.amountCount += order.OrderDetails.Select(
                            d => d.UnitPrice ?? 0m * d.Quantity ?? 0
                        ).ToList().Sum() - (order.Voucher?.DiscountAmount ?? 0m);
                    //sold_count 
                    output.soldCount += order.OrderDetails.Count;
                }

                return output;
            }

            decimal CalculatePercentage(dynamic inRage, dynamic beforeRange)
            {
                if ((inRage == 0 && beforeRange == 0) || inRage == beforeRange)
                {
                    return 0;
                }

                if(inRage == 0)
                {
                    return -100;
                }

                if(beforeRange == 0)
                {
                    return 100;
                }

                decimal result = (inRage - beforeRange) / beforeRange * 100;

                return Math.Round(result, 2); // có khói r
            }

            ICollection<string> CalculateLabels(DateTime dateFrom, DateTime dateEnd)
            {
                int numberOfDays = (dateEnd - dateFrom).Days + 1; // Số ngày trong khoảng thời gian

                /*int spaceDateLabel = 0;
                if(numberOfDays > 10)
                {
                    spaceDateLabel = numberOfDays / 10;
                }*/

                List<string> dateList = new List<string>();

                DateTime currentDate = dateFrom; // Ngày hiện tại

                for (int i = 0; i < numberOfDays; i++)
                {
                    if (currentDate.Date <= dateEnd.Date)
                    {
                        dateList.Add(currentDate.Date.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        break;
                    }

                    //currentDate = spaceDateLabel == 0 ? currentDate.AddDays(1) : currentDate.AddDays(spaceDateLabel);
                    currentDate = currentDate.AddDays(1);
                }

                return dateList;
            }

            ICollection<DashboardDataSets> CalculateDataSets(ICollection<Order> orders, DateTime dateFrom, DateTime dateEnd)
            {
                List<DashboardDataSets> dataSets = new List<DashboardDataSets>();
                // get orderCount each date
                DashboardDataSets orderData = new DashboardDataSets
                {
                    Label = "Order"
                };
                // get ammount count each date
                DashboardDataSets amountData = new DashboardDataSets
                {
                    Label = "Estimated Amount"
                };
                // get sold count each date
                DashboardDataSets soldData = new DashboardDataSets
                {
                    Label = "Number of foods sold"
                };

                int numberOfDays = (dateEnd - dateFrom).Days + 1;

                DateTime currentDate = dateFrom; // Ngày hiện tại

                for (int i = 0; i < numberOfDays; i++)
                {
                    if (currentDate.Date <= dateEnd.Date)
                    {
                        var orderCurrentDate = orders.Where(x => x.OrderDate.Value.Date == currentDate.Date).ToList();

                        // get order each date
                        var orderCount = orderCurrentDate.Count;
                        orderData.Data.Add(orderCount);

                        // amount each date
                        var amount = orderCurrentDate.Select(
                                x => x.OrderDetails.Select(
                                    d => d.UnitPrice ?? 0m * d.Quantity ?? 0
                                ).ToList().Sum() - (x.Voucher?.DiscountAmount ?? 0m)
                            ).ToList().Sum();
                        amountData.Data.Add(amount);
                        // sold each date

                        var sold = orderCurrentDate.Select(
                                x => x.OrderDetails.Count
                            ).ToList().Sum();
                        soldData.Data.Add(sold);
                    }
                    else
                    {
                        break;
                    }

                    currentDate = currentDate.AddDays(1);
                }

                dataSets.Add(orderData);
                dataSets.Add(amountData);
                dataSets.Add(soldData);

                return dataSets;
            }
        }
    }
}
