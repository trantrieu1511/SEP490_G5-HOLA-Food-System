using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HFS_BE.DAO.AdminDao
{
	public class AdminDao : BaseDao
	{
		public AdminDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public BaseOutputDto CreateAdmin(RegisterDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

			var mesall = context.Admins.ToList();
			int cuschinhId = 0;
			if (mesall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.Admins.OrderBy(s => s.AdminId).Last();
				string cusIdCheck = cus.AdminId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}

			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "AD" + paddedString.Substring(2);

			if (!isValid)
			{
				string err = "";
				foreach (var item in validationResults)
				{
					err += item.ToString() + " ";
				}
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, err);
			}
			var data = context.Customers.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			var data2 = context.Sellers.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			var data3 = context.PostModerators.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			var data4 = context.MenuModerators.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			var data5 = context.Admins.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			var data6 = context.Accountants.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			if (data != null || data2 != null || data3 != null||data4!=null||data5!=null||data6!=null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Email đã sử dụng");
			}
			var userCreate = new HFS_BE.Models.Admin
			{
				AdminId = paddedString,
				Email = model.Email,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.Gender,
				ConfirmedEmail = true,

			};



			using (HMACSHA256? hmac = new HMACSHA256())
			{
				userCreate.PasswordSalt = hmac.Key;
				userCreate.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
			}

			try
			{
				context.Admins.Add(userCreate);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public List<Admin>? GetAdmins()
		{
			try
			{
				return context.Admins.ToList();
			}
			catch (Exception)
			{

				return null;
			}
		}

		public List<DashboadPieAdminOutputDto> GetDashBoadPie()
		{
			try
			{
				List<DashboadPieAdminOutputDto> list = new List<DashboadPieAdminOutputDto>();
				int seller = context.Sellers.Count();
				int cus= context.Customers.Count();
				int shipper = context.Shippers.Count();
				var sellerd= new DashboadPieAdminOutputDto(
					actor:"Seller",
					total:seller
					
					);
				var cusd = new DashboadPieAdminOutputDto(
					actor: "Customer",
					total: cus

					);
				var shipperd = new DashboadPieAdminOutputDto(
					actor: "Shipper",
					total: shipper

					);
				list.Add(sellerd);
				list.Add(cusd);
				list.Add(shipperd);
				
				return list;

			}
			catch (Exception)
			{

				return null;
			}
		}
		public List<DashBoardAdminLineOutputDto> GetDashBoadLine(DashBoardAdminLineInputDto inputDto)
		{
			try
			{
				List<DashBoardAdminLineOutputDto> list = new List<DashBoardAdminLineOutputDto>();
				

				foreach(var date in inputDto.dates)
				{
					var data = context.Sellers.Where(s => s.CreateDate.Value.Date == date.Date).Count();
					var sellerd = new DashBoardAdminLineOutputDto();
					sellerd.CreateDate = date;
					sellerd.Data = data;
					sellerd.User = "Seller";
					list.Add(sellerd);
					var data2 = context.Customers.Where(s => s.CreateDate.Value.Date == date.Date).Count();
					var cusd = new DashBoardAdminLineOutputDto();
					cusd.CreateDate = date;
					cusd.Data = data2;
					cusd.User = "Customer";
					list.Add(cusd);
					var data3 = context.Shippers.Where(s => s.CreateDate.Value.Date == date.Date).Count();
					var shipperd = new DashBoardAdminLineOutputDto();
					shipperd.CreateDate = date;
					shipperd.Data = data3;
					shipperd.User = "Shipper";
					list.Add(shipperd);

				}
				
				


				return list;

			}
			catch (Exception)
			{

				return null;
			}
		}
		public DashboadAdminOutputDto GetDashBoadAdminTotal()
		{
			try

			{
				DashboadAdminOutputDto outputDto = new DashboadAdminOutputDto();
				int sellervetify = context.Sellers.Where(s => s.Status == 1).Count();
				int sellerban = context.Sellers.Where(s => s.IsBanned == true).Count();
				int totalseller = context.Sellers.Count();
				int totalcus = context.Customers.Count();
				int seller = context.Sellers.Where(s => s.IsBanned == false && s.Status == 1).Count();
				int cusban = 0;
				int shipper = 0;
				int shipperban = context.Shippers.Where(s => s.Status == 1 &&s.ManageBy!=null).Count();
				int shippervetify = context.Shippers.Where(s => s.Status == 1).Count();
				int totalshipper= context.Shippers.Count();
	            int totalreport= context.SellerReports.Count();
				int solvereport = context.SellerReports.Where(s=>s.Status==1).Count();
				int pending = context.SellerReports.Where(s => s.Status == 0).Count();
				outputDto.ManagedShipper = shipperban;
				outputDto.BanSeller = sellerban;
				outputDto.TotalCustomer = totalcus;
				outputDto.TotalSeller = totalseller;
				outputDto.TotalShippper = totalshipper;
				outputDto.VetifySeller = sellervetify;
				outputDto.VetifyShipper = shippervetify;
				outputDto.SolvedReport = solvereport;
				outputDto.TotalReport = totalreport;
				outputDto.PendingReport = pending;
				outputDto.RejectReport = totalreport- pending- solvereport;
				return outputDto;

			}
			catch (Exception)
			{

				return null;
			}
		}
	}
}
