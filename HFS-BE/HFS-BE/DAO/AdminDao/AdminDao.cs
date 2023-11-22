using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using HFS_BE.Utils;
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
			if (data != null || data2 != null || data3 != null||data4!=null||data5!=null)
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
				int seller = context.Sellers.Where(s => s.IsBanned == false&&s.IsVerified==true).Count();
				int cus= context.Customers.Where(s => s.IsBanned == false).Count();
				int shipper= context.Shippers.Where(s => s.IsBanned == false&&s.IsVerified==true).Count();
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
		
		public List<DashboadPieAdminOutputDto> GetDashBoadAdminOrder(List<DashboadAdminInputOrderDto> input)
		{
			try
			{
				DateTime date = DateTime.Now;
				
               var listr = context.MenuReports.Where(s => s.CreateDate.Month == date.Month).ToList();

				return null;

			}
			catch (Exception)
			{

				return null;
			}
		}
	}
}
