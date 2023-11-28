using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.SellerReportBL;
using HFS_BE.DAO.SellerReportDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ReportSeller
{

	public class ReportSellerController : BaseController
	{
		public ReportSellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("users/reportseller")]
		
		public BaseOutputDto CreateReport([FromForm]SellerReportBLInputDto input)
		{
			try
			{
			
				var business = this.GetBusinessLogic<SellerReportBL>();
				input.ReportBy = this.GetUserInfor().UserId;
				var output = business.CreateReport(input);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[HttpPost("users/listreportseller")]

		public ListSellerReportOutputDtoBL ListSellerReport()
		{
			try
			{

				var business = this.GetBusinessLogic<SellerReportBL>();
			
				var output = business.GetAllSellerReport();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[HttpPost("users/updatereportseller")]

		public BaseOutputDto UpdateSellerReport(SellerReportByAdminInputDto inputDto)
		{
			try
			{

				var business = this.GetBusinessLogic<SellerReportBL>();
				inputDto.UpdateBy = GetUserInfor().UserId;
				var output = business.UpdateReport(inputDto);
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
