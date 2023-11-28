using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.SellerReportDao
{
	public class SellerReportDao : BaseDao
	{
		public SellerReportDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}



		public BaseOutputDto CreateReportSeller(SellerReportInputDto inputDto)
		{
			try
			{
				var report = new SellerReport()
				{
					SellerId = inputDto.SellerId,
					CreateDate = DateTime.Now,
					ReportBy = inputDto.ReportBy,
					ReportContent = inputDto.ReportContent,
					Status = 0
				};
				this.context.Add(report);
				this.context.SaveChanges();

				foreach (var img in inputDto.Images)
				{
					context.Add(new SellerReportImage
					{
						SellerReportId = report.SellerReportId,
						Path = img
					});
					context.SaveChanges();
				}
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception e)
			{

				return this.Output<BaseOutputDto> (Constants.ResultCdFail);
			}
		}
		public ListSellerReportOutputDto GetAllSellerReport()
		{
			try
			{
				List<SellerReportOutputDto> postsModel = context.SellerReports
										.Include(p => p.SellerReportImages)
											.Include(p => p.Seller)
												.Include(p => p.ReportByNavigation)
										.Select(p => new SellerReportOutputDto
										{
											SellerReportId = p.SellerReportId,
											SellerId = p.SellerId,
											CreateDate = p.CreateDate,
											ReportBy = p.ReportBy,
											ReportContent = p.ReportContent,
											Note = p.Note,
											Status = p.Status,
											UpdateBy = p.UpdateBy,
											UpdateDate = p.UpdateDate,
											SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
											ShopName = p.Seller.ShopName ,
											ReportByName = p.ReportByNavigation.FirstName + " " + p.ReportByNavigation.LastName,
											Images = p.SellerReportImages.ToList()
										}).OrderByDescending(s => s.CreateDate)
										.ToList();
				var output = this.Output<ListSellerReportOutputDto>(Constants.ResultCdSuccess);
				output.data = postsModel;
				return output;
			}
			catch (Exception e)
			{
				return this.Output<ListSellerReportOutputDto>(Constants.ResultCdFail);
			}
		}
		public ListSellerReportOutputDto GetAllSellerReportByCustomer(SellerReportByCustomerInputDto inputDto)
		{
			try
			{
				List<SellerReportOutputDto> postsModel = context.SellerReports
										.Include(p => p.SellerReportImages)
											.Include(p => p.Seller)
												.Include(p => p.ReportByNavigation)
												.Where(s => s.ReportBy == inputDto.ReportBy)
										.Select(p => new SellerReportOutputDto
										{
											SellerReportId = p.SellerReportId,
											SellerId = p.SellerId,
											CreateDate = p.CreateDate,
											ReportBy = p.ReportBy,
											ReportContent = p.ReportContent,
											Note = p.Note,
											Status = p.Status,
											UpdateBy = p.UpdateBy,
											UpdateDate = p.UpdateDate,
											SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
											ReportByName = p.ReportByNavigation.FirstName + " " + p.ReportByNavigation.LastName,
											Images = p.SellerReportImages.ToList()
										}).OrderByDescending(s => s.CreateDate)
										.ToList();
				var output = this.Output<ListSellerReportOutputDto>(Constants.ResultCdSuccess);
				output.data = postsModel;
				return output;
			}
			catch (Exception e)
			{
				return this.Output<ListSellerReportOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto UpdateSellerReport(SellerReportByAdminInputDto input)
		{
			try
			{
				var data = context.SellerReports.FirstOrDefault(s => s.SellerReportId == input.SellerReportId);
				data.UpdateDate = DateTime.Now;
				data.UpdateBy = input.UpdateBy;
				data.Status = input.Status;
				data.Note = input.Note;
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess); 
			}
			catch (Exception e)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
