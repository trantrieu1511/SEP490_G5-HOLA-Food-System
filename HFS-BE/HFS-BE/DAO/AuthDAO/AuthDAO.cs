using AutoMapper;
using Google.Apis.Auth;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace HFS_BE.Dao.AuthDao
{
	public class AuthDao : BaseDao
	{
		private const string MailgunApiBaseUrl = "https://api.mailgun.net/v3/";
		private const string MailgunDomain = "sandbox38179487b9c441e69a66b0ecb5364d85.mailgun.org";
		private const string MailgunApiKey = "138645612dfd388290c47d43875d34d6-8c9e82ec-3d2cebe3";

        public AuthDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public AuthDaoOutputDto Login(AuthDaoInputDto input)
		{
			var output = new AuthDaoOutputDto();
			var user = context.Customers.Where(s => s.Email == input.Email).FirstOrDefault();
			
			if (user == null)
			{
				return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
			}

			var match = CheckPassword(input.Password, (Customer)user);

			if (!match)
			{
				return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Email Or Password Was Invalid");
			}
			//if (user.IsBanned == true)
			//{
			//	return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
			//}
			var dapmapper = mapper.Map<Customer, LoginGoogleInputDto>(user);
			JwtSecurityToken token = GenerateSecurityToken(dapmapper);
			output.Token = new JwtSecurityTokenHandler().WriteToken(token);
			output.UserId = user.CustomerId;


            return output;

		}
		
		private bool CheckPassword(string password, Customer user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}

		//private dynamic GenerateSecurityToken(User user)
		//{
		//	var conf = new ConfigurationBuilder()
		//		.SetBasePath(Directory.GetCurrentDirectory())
		//		.AddJsonFile("appsettings.json", true, true)
		//		.Build();

		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var key = Encoding.ASCII.GetBytes(conf["JWT:Secret"]);



		//	var authSigningKey = new SymmetricSecurityKey(key);
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, user.RoleId.ToString()),
		//				}),
		//		Expires = DateTime.UtcNow.AddDays(7),
		//		SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		//	};
		//	var token = tokenHandler.CreateToken(tokenDescriptor);
		//	var encrypterToken = tokenHandler.WriteToken(token);


		//	return new { token = encrypterToken, username = user.Email };
		//}

		public JwtSecurityToken GenerateSecurityToken(LoginGoogleInputDto acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.UserId.Substring(0, 2); ;
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Name, acc.FirstName + acc.LastName),
				new Claim("userId", acc.UserId.ToString()),
			new Claim(ClaimTypes.Role,role)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddMinutes(15), 
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}

		public async Task<BaseOutputDto> RegisterCustomer(RegisterDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);
			var cusall = context.Customers.ToList();
			int cuschinhId = 0;
			if (cusall.Count == 0)
			{
				cuschinhId = 1;
			}
			else
			{
				var cus = context.Customers.OrderBy(s => s.CustomerId).Last();
				string cusIdCheck = cus.CustomerId;
				string trimmedString = cusIdCheck.Substring(2);
				int CusIdiNT = Int32.Parse(trimmedString);
				cuschinhId = CusIdiNT + 1;

			}
		
			int desiredLength = 12;
			char paddingChar = '0';
			string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
			paddedString = "CU" + paddedString.Substring(2);

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
			var user = new HFS_BE.Models.Customer
			{
				CustomerId = paddedString,
				Email = model.Email,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				Gender = model.Gender,
				ConfirmedEmail = false,
				//IsBanned=false
				
			};



			using (HMACSHA256? hmac = new HMACSHA256())
			{
				user.PasswordSalt = hmac.Key;
				user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
			}

			try
			{
				context.Customers.Add(user);

				context.SaveChanges();
				ForgotPasswordInputDto forgot = new ForgotPasswordInputDto();
				forgot.Email = user.Email;
				await SendVetifyPasswordtoEmailAsync(forgot);
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

	
		public BaseOutputDto ForgotPassword(ForgotPasswordInputDto model)
		{
			var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

			if (!isValid)
			{
				string err = "";
				foreach (var item in validationResults)
				{
					err += item.ToString() + " ";
				}
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, err);
			}



			try
			{

				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public async Task<BaseOutputDto> SendForgotPasswordtoEmailAsync(ForgotPasswordInputDto model)
		{
			string userid = "";
			string confirmationCode = GenerateConfirmationCode(model.Email);
			//using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
			//{
			//	var user = context.Users.Where(s => s.Email.ToLower().Equals(toEmail.ToLower())).FirstOrDefault();

			//	if (user == null)
			//	{
			//		return BadRequest();
			//	}
			//	userid = user.UserId.ToString();
			//}

			string subject = "Xác nhận thay đổi trạng thái";
			string message = $"Vui lòng nhấp vào liên kết sau để xác nhận thay đổi trạng thái: {GetForgotPasswordLink("1", confirmationCode)}";

			try
			{
				
				await SendEmail2Async(model.Email, subject, message);
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}

		}
		public async Task<BaseOutputDto> SendVetifyPasswordtoEmailAsync(ForgotPasswordInputDto model)
		{
			string userid = "";
			string confirmationCode = GenerateConfirmationCode(model.Email);
			//using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
			//{
			//	var user = context.Users.Where(s => s.Email.ToLower().Equals(toEmail.ToLower())).FirstOrDefault();

			//	if (user == null)
			//	{
			//		return BadRequest();
			//	}
			//	userid = user.UserId.ToString();
			//}

			string subject = "Xác nhận thay đổi trạng thái";
			string message = $"Vui lòng nhấp vào liên kết sau để xác nhận thay đổi trạng thái: {GetConfirmEmailLink("1", confirmationCode)}";

			try
			{
				await SendEmail2Async(model.Email, subject, message);
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}

		}
		private async Task<bool> SendEmail2Async(string toEmail,string subject, string content)
		{
			try
			{
				string from = "holafoodfpt@gmail.com";
				string pass = "wqsq fqmv iwhu ablr";
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient("smtp.gmail.com");

				mail.To.Add(toEmail);
				mail.From = new MailAddress(from);
				mail.Subject = subject;
				mail.Body = "HOLA FOOD:" + content;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Credentials = new NetworkCredential(from, pass);
				await smtp.SendMailAsync(mail);
				return true;
			}
			catch
			{
				return false;
			}

		}

		private async Task SendEmail(string toEmail, string subject, string message)
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{MailgunApiKey}")));

			var content = new FormUrlEncodedContent(new[]
			{
			new KeyValuePair<string, string>("from", "lunguyen2k18@gmail.com"),
			new KeyValuePair<string, string>("to", toEmail),
			new KeyValuePair<string, string>("subject", subject),
			new KeyValuePair<string, string>("text", message)
		});

			var response = await httpClient.PostAsync($"{MailgunApiBaseUrl}{MailgunDomain}/messages", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Failed to send email.");
			}
		}
		private string GetForgotPasswordLink(string userId, string confirmationCode)
		{
			string baseUrl = "http://localhost:4200/#/forgot";
			//	var query = new Dictionary<string, string>
			//{
			//	{ "userId", userId },
			//	{ "code", confirmationCode }
			//};
			var confirmationLink = baseUrl + "?userId=" + userId + "&code=" + confirmationCode;
			return confirmationLink;
		}

		private string GetConfirmEmailLink(string userId, string confirmationCode)
		{
			string baseUrl = "http://localhost:4200/#/confirm";
			//	var query = new Dictionary<string, string>
			//{
			//	{ "userId", userId },
			//	{ "code", confirmationCode }
			//};
			var confirmationLink = baseUrl + "?userId=" + userId + "&code=" + confirmationCode;
			return confirmationLink;
		}
		private string GenerateConfirmationCode(string userId)//tạo ra mã đễ 

		{
			var conf = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", true, true)
			.Build();
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(conf["JWT:Secret"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("userId", userId) }),
				Expires = DateTime.UtcNow.AddMinutes(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var confirmationCode = tokenHandler.WriteToken(token);

			return confirmationCode;
		}
		private string ValidateConfirmationCodeForgot(string confirmationCode)
		{
			
			var conf = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", true, true)
			.Build();
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(conf["JWT:Secret"]);

			try
			{
				tokenHandler.ValidateToken(confirmationCode, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				string email = jwtToken.Claims.First(c => c.Type == "userId").Value;
				using (var context = new SEP490_HFS_2Context())
				{
					var data = context.Customers.Where(s => s.Email == email).FirstOrDefault();
					var data1 = context.Sellers.Where(s => s.Email == email).FirstOrDefault();
					var data2 = context.Shippers.Where(s => s.Email == email).FirstOrDefault();
					//var data3 = context.PostModerators.Where(s => s.Email == email).FirstOrDefault();
					//var data4 = context.MenuModerators.Where(s => s.Email == email).FirstOrDefault();
					if (data != null)
					{
						return email;


					}
					else if (data1 != null)
					{
						return email;

					}
					else if (data2 != null)
					{
						return email;

					}
					//else if (data3 != null)
					//{
					//	return email;

					//}
					//else if (data4 != null)
					//{
					//	return email;

					//}

				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		public BaseOutputDto ValidateConfirmationCode(ConfirmForgotPasswordInputDto model)
		{
			
			var conf = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", true, true)
			.Build();
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(conf["JWT:Secret"]);

			try
			{
				tokenHandler.ValidateToken(model.confirm, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				string email = jwtToken.Claims.First(c => c.Type == "userId").Value;

				var data = context.Customers.Where(s => s.Email == email).FirstOrDefault();
				var data1 = context.Sellers.Where(s => s.Email == email).FirstOrDefault();
				var data2 = context.Shippers.Where(s => s.Email == email).FirstOrDefault();
				if (data != null)
				{
					data.ConfirmedEmail = true;
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);


				}
				else if (data1 != null)
				{
					data1.ConfirmedEmail = true;
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}
				else if (data2 != null)
				{
					data2.ConfirmedEmail = true;
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
			catch
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto ValidateConfirmationCodeForgot(ConfirmForgotPasswordInputDto model)
		{

			var conf = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", true, true)
			.Build();
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(conf["JWT:Secret"]);

			try
			{
				tokenHandler.ValidateToken(model.confirm, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				string email = jwtToken.Claims.First(c => c.Type == "userId").Value;

				var data = context.Customers.Where(s => s.Email == email).FirstOrDefault();
				var data1 = context.Sellers.Where(s => s.Email == email).FirstOrDefault();
				var data2 = context.Shippers.Where(s => s.Email == email).FirstOrDefault();
				var data3 = context.PostModerators.Where(s => s.Email == email).FirstOrDefault();
				var data4 = context.MenuModerators.Where(s => s.Email == email).FirstOrDefault();
				if (data != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
					

				}
				else if (data1 != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}else if (data2 != null) 
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}
				else if (data3 != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}
				else if (data4 != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				}


				return this.Output<BaseOutputDto>(Constants.ResultCdFail);

			}
			catch
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto ChangePassword (ChangeForgotPasswordInputDto change)
		{

			var conf = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", true, true)
			.Build();
			

			try
			{
				string email = ValidateConfirmationCodeForgot(change.confirm);
				if (email == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Token expires to change password");
				}
				var data = context.Customers.Where(s => s.Email == email).FirstOrDefault();
				var data1 = context.Sellers.Where(s => s.Email == email).FirstOrDefault();
				var data2 = context.Shippers.Where(s => s.Email == email).FirstOrDefault();
				var data3 = context.PostModerators.Where(s => s.Email == email).FirstOrDefault();
				var data4 = context.MenuModerators.Where(s => s.Email == email).FirstOrDefault();
				if (change.Password != change.ConfirmPassword)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "password and confirm password are different");
				}
				if (data != null)
				{
					using (HMACSHA256? hmac = new HMACSHA256())
					{
						data.PasswordSalt = hmac.Key;
						data.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(change.Password));
					}
					context.Customers.Update(data);
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				}
				else if (data1 != null)
				{
					using (HMACSHA256? hmac = new HMACSHA256())
					{
						data1.PasswordSalt = hmac.Key;
						data1.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(change.Password));
					}
					context.Sellers.Update(data1);
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				}
				else if( data2 != null)
				{
					using (HMACSHA256? hmac = new HMACSHA256())
					{
						data2.PasswordSalt = hmac.Key;
						data2.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(change.Password));
					}
					context.Shippers.Update(data2);
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				}
				else if (data3 != null)
				{
					using (HMACSHA256? hmac = new HMACSHA256())
					{
						data3.PasswordSalt = hmac.Key;
						data3.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(change.Password));
					}
					context.PostModerators.Update(data3);
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				}
				else if (data4 != null)
				{
					using (HMACSHA256? hmac = new HMACSHA256())
					{
						data4.PasswordSalt = hmac.Key;
						data4.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(change.Password));
					}
					context.MenuModerators.Update(data4);
					context.SaveChanges();
					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				}
				else
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail);
				}

				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
			
		}

		public async Task<AuthDaoOutputDto>? LoginWithGoogleAsync(string credential)
		{
			var conf = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			var output = new AuthDaoOutputDto();
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { (conf["ApplicationSettings:GoogleClientId"]) }
			};

			var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

			var user = context.Customers.Where(x => x.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
			var data5 = context.Shippers.Where(s => s.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
			var data2 = context.Sellers.Where(s => s.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
			var data3 = context.PostModerators.Where(s => s.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
			var data4 = context.MenuModerators.Where(s => s.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
			//var data5 = context.Admins.Where(s => s.Email.ToLower() == payload.Email.ToLower()).FirstOrDefault();
	
			if (user != null)
			{
				//if (user.IsBanned == true)
				//{
				//	return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
				//}

				var dapmapper = mapper.Map<Customer, LoginGoogleInputDto>(user);
				JwtSecurityToken token = GenerateSecurityToken(dapmapper);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else if (data2 != null)
			{
				if (data2.IsBanned == true)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
				}
				var dapmapper = mapper.Map<Seller, LoginGoogleInputDto>(data2);
				JwtSecurityToken token = GenerateSecurityToken(dapmapper);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else if (data3 != null)
			{
				if (data3.IsBanned == true)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
				}
				var dapmapper = mapper.Map<PostModerator, LoginGoogleInputDto>(data3);
				JwtSecurityToken token = GenerateSecurityToken(dapmapper);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else if (data4 != null)
			{
				if (data4.IsBanned == true)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
				}
				var dapmapper = mapper.Map<MenuModerator, LoginGoogleInputDto>(data4);
				JwtSecurityToken token = GenerateSecurityToken(dapmapper);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else if (data5 != null)
			{
				if (data5.IsBanned == true)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "You have been banned, please contact us to resolve");
				}
				var dapmapper = mapper.Map<Shipper, LoginGoogleInputDto>(data5);
				JwtSecurityToken token = GenerateSecurityToken(dapmapper);
				output.Token = new JwtSecurityTokenHandler().WriteToken(token);
				return output;
			}
			else
			{
				var cusall = context.Customers.ToList();
				int cuschinhId = 0;
				if (cusall.Count == 0)
				{
					cuschinhId = 1;
				}
				else
				{
					var cus = context.Customers.OrderBy(s => s.CustomerId).Last();
					string cusIdCheck = cus.CustomerId;
					string trimmedString = cusIdCheck.Substring(2);
					int CusIdiNT = Int32.Parse(trimmedString);
					cuschinhId = CusIdiNT + 1;

				}

				int desiredLength = 12;
				char paddingChar = '0';
				string paddedString = cuschinhId.ToString().PadLeft(desiredLength - 2, paddingChar);
				paddedString = "CU" + paddedString.Substring(2);

				string[] FullName = payload.Name.Split(" ");
				var user1 = new HFS_BE.Models.Customer {CustomerId= paddedString, Email = payload.Email, FirstName = FullName[0], LastName = FullName[1], Gender = "Null", ConfirmedEmail=true};
				if (FullName.Length == 3)
				{
					user1 = new HFS_BE.Models.Customer { CustomerId = paddedString, Email = payload.Email, FirstName = FullName[0], LastName = FullName[1] + FullName[2], Gender = "Null", ConfirmedEmail = true };

				}





				using (HMACSHA256? hmac = new HMACSHA256())
				{
					user1.PasswordSalt = hmac.Key;
					user1.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload.Email));
				}
				try
				{
					await context.Customers.AddAsync(user1);
					context.SaveChangesAsync();
					
					var dapmapper = mapper.Map<Customer, LoginGoogleInputDto>(user1);
					JwtSecurityToken token = GenerateSecurityToken(dapmapper);
					output.Token = new JwtSecurityTokenHandler().WriteToken(token);
					return output;
				}
				catch (Exception ex)
				{
					return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail);
				}
			}


		}

	}
}
