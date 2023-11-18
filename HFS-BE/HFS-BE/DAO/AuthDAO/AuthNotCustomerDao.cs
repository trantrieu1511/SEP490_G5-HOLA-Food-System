using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HFS_BE.DAO.AuthDAO
{
	public class AuthNotCustomerDao : BaseDao
	{
		public AuthNotCustomerDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public AuthDaoOutputDto LoginNotCustomer(AuthDaoInputDto input)
		{
			var output = new AuthDaoOutputDto();
			var user = context.Sellers.Where(s => s.Email == input.Email).FirstOrDefault();
			if (user != null)
			{
				var match = CheckPasswordSeller(input.Password, (Seller)user);

				if (!match)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
				}
				if (user.IsBanned == true)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned due to violations, please contact us to resolve!");
				}
				JwtSecurityToken token = GenerateSecurityTokenSeller((Seller)user);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else
			{
				var shipper = context.Shippers.Where(s => s.Email == input.Email).FirstOrDefault();
			
				if (shipper == null)
				{
					var menuModerators = context.MenuModerators.Where(s => s.Email == input.Email).FirstOrDefault();
					
					if (menuModerators != null)
					{
						if (menuModerators.IsBanned == true)
						{
							return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned due to violations, please contact us to resolve!");
						}
						var match = CheckPasswordModerator(input.Password, menuModerators);

						if (!match)
						{
							return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
						}
						JwtSecurityToken token = GenerateSecurityTokenModerator(menuModerators);
						output.Token = new JwtSecurityTokenHandler().WriteToken(token);
						return output;
					}
					else
					{
						var postModerators = context.PostModerators.Where(s => s.Email == input.Email).FirstOrDefault();
					
						if (postModerators != null)
						{
							if (postModerators.IsBanned == true)
							{
								return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned due to violations, please contact us to resolve!");
							}
							var match = CheckPasswordPostM(input.Password, postModerators);

							if (!match)
							{
								return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
							}
							JwtSecurityToken token = GenerateSecurityTokenModerator(postModerators);
							output.Token = new JwtSecurityTokenHandler().WriteToken(token);
							return output;
						}
						else
						{
							var admin = context.Admins.Where(s => s.Email == input.Email).FirstOrDefault();
							if (admin != null)
							{
								var match = CheckPasswordAdmin(input.Password, admin);

								if (!match)
								{
									return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
								}
								JwtSecurityToken token = GenerateSecurityTokenAdmin(admin);
								output.Token = new JwtSecurityTokenHandler().WriteToken(token);
								return output;
							}
							else
							{
								return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
							}

						}
					}
				}
				else
				{
					var match = CheckPasswordShipper(input.Password, shipper);
					if (shipper.IsBanned == true)
					{
						return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned due to violations, please contact us to resolve!");
					}
					if (!match)
					{
						return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
					}
					JwtSecurityToken token = GenerateSecurityTokenShipper(shipper);
					output.Token = new JwtSecurityTokenHandler().WriteToken(token);
					return output;
				}
				


			}
				 
			



		}

		public BaseOutputDto RegisterSeller(RegisterSellerDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);
			var seall = context.Sellers.ToList();
			int cuschinhId = 0;
			if (seall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.Sellers.OrderBy(s => s.SellerId).Last();
				string cusIdCheck = cus.SellerId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}

			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "SE" + paddedString.Substring(2);

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
			if (data != null || data2 != null || data3 != null || data4 != null || data5 != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Email has been used");
			}
			model.BirthDate = model.BirthDate.Value.AddDays(1);
			var user = new HFS_BE.Models.Seller
			{
				SellerId = paddedString,
				Email = model.Email,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.Gender,
				PhoneNumber = model.PhoneNumber,
				ShopName=model.ShopName,
				ShopAddress=model.ShopAddress,
				ConfirmedEmail = false,
				IsBanned = false

			};



			using (HMACSHA256? hmac = new HMACSHA256())
			{
				user.PasswordSalt = hmac.Key;
				user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
			}

			try
			{
				context.Sellers.Add(user);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto RegisterShipper(RegisterDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);
			var seall = context.Shippers.ToList();
			int cuschinhId = 0;
			if (seall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.Shippers.OrderBy(s => s.ShipperId).Last();
				string cusIdCheck = cus.ShipperId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}

			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "SH" + paddedString.Substring(2);

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
			if (data != null || data2 != null || data3 != null || data4 != null || data5 != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Email đã sử dụng");
			}
			//var userCreate = new HFS_BE.Models.Admin
			//{
			//	AdminId = paddedString,
			//	Email = model.Email,
			//	BirthDate = model.BirthDate,
			//	FirstName = model.FirstName,
			//	LastName = model.LastName,
			//	Gender = model.Gender,
			//	ConfirmedEmail = true,

			//};
			model.BirthDate = model.BirthDate.Value.AddDays(1);
			var user = new HFS_BE.Models.Shipper
			{
				ShipperId = paddedString,
				Email = model.Email,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.Gender,
				ConfirmedEmail = false,
				IsBanned = false

			};
			using (HMACSHA256? hmac = new HMACSHA256())
			{
				user.PasswordSalt = hmac.Key;
				user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
			}

			try
			{
				context.Shippers.Add(user);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		private bool CheckPasswordSeller(string password, Seller user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}
		private bool CheckPasswordShipper(string password, Shipper user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}
		private bool CheckPasswordPostM(string password, PostModerator user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}
		private bool CheckPasswordAdmin(string password, Admin user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}
		private JwtSecurityToken GenerateSecurityTokenSeller(Seller acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.SellerId.Substring(0, 2).ToString(); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.SellerId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddMinutes(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		private JwtSecurityToken GenerateSecurityTokenShipper(Shipper acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.ShipperId.Substring(0, 2).ToString(); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.ShipperId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddHours(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		private bool CheckPasswordModerator(string password, MenuModerator user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}

		private JwtSecurityToken GenerateSecurityTokenModerator (MenuModerator acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.ModId.Substring(0, 2).ToString(); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.ModId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddHours(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		private JwtSecurityToken GenerateSecurityTokenModerator(PostModerator acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.ModId.Substring(0, 2).ToString(); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.ModId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddHours(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		private JwtSecurityToken GenerateSecurityTokenAdmin(Admin acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.AdminId.Substring(0, 2).ToString(); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.AdminId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddHours(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
	}
}
