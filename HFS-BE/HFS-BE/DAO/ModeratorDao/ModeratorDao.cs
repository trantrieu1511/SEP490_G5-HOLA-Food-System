using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HFS_BE.DAO.ModeratorDao
{
	public class ModeratorDao : BaseDao
	{
		public ModeratorDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListPostModeratorDtoOutput GetAllPostModerator()
		{
			try
			{
				var user = this.context.PostModerators.ToList();

				var output = this.Output<ListPostModeratorDtoOutput>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<PostModerator>, List<PostModeratorDtoOutput>>(user);

				return output;
			}
			catch (Exception)
			{
				return this.Output<ListPostModeratorDtoOutput>(Constants.ResultCdFail);
			}
		}

		public ListMenuModeratorDtoOutput GetAllMenuModerator()
		{
			try
			{
				var user = this.context.MenuModerators.ToList();

				var output = this.Output<ListMenuModeratorDtoOutput>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<MenuModerator>, List<MenuModeratorDtoOutput>>(user);

				return output;
			}
			catch (Exception)
			{
				return this.Output<ListMenuModeratorDtoOutput>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto CreateMenuModerator(RegisterDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

			var mesall = context.MenuModerators.ToList();
			int cuschinhId = 0;
			if (mesall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.MenuModerators.OrderBy(s => s.ModId).Last();
				string cusIdCheck = cus.ModId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}

			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "MM" + paddedString.Substring(2);

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
			if (data != null || data2 != null || data3 != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Email đã sử dụng");
			}
			var userCreate = new HFS_BE.Models.MenuModerator
			{
				ModId = paddedString,
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
				context.MenuModerators.Add(userCreate);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto CreatePostModerator(RegisterDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

			var posall = context.PostModerators.ToList();
			int cuschinhId = 0;
			if (posall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.PostModerators.OrderBy(s => s.ModId).Last();
				string cusIdCheck = cus.ModId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}

			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "PM" + paddedString.Substring(2);

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
			var data3 = context.MenuModerators.Where(s => s.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
			if (data != null||data2!=null||data3!=null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Email đã sử dụng");
			}
			var userCreate = new HFS_BE.Models.PostModerator
			{
				ModId = paddedString,
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
				context.PostModerators.Add(userCreate);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}
