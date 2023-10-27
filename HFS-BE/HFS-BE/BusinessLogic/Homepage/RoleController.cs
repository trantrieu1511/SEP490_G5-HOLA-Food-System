using AutoMapper;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace HFS_BE.Controllers.Homepage
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{

		IConfiguration _configuration;
		private const string MailgunApiBaseUrl = "https://api.mailgun.net/v3/";
		private const string MailgunDomain = "sandbox38179487b9c441e69a66b0ecb5364d85.mailgun.org";
		private const string MailgunApiKey = "c050ad11536d134d879a655d65baae5d-5465e583-034be4a6";
		private const string SecretKey = "YOUR_SECRET_KEY";
		private readonly IPhotoService _photoService;
		public RoleController(IConfiguration configuration, IPhotoService photoService)
		{
			_configuration = configuration;
			_photoService = photoService;
		}
		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			try
			{
				using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
				{
					var data = context.Roles.ToList();
					if (data == null)
					{
						return NotFound();
					}
					return Ok(data);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[HttpPost]
		public IActionResult RequiredToken(AuthDaoInputDto request)
		{
			using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
			{
				//Include(s => s.Customers)
				User acc = context.Users.FirstOrDefault(u => u.Email == request.Email);
				if (acc == null)
				{
					return Unauthorized("Username or Password incorrect!");
				}
				bool check = CheckPassword(request.Password, acc);
				if (check == false)
				{
					return Unauthorized("Username or Password incorrect!");
				}
				JwtSecurityToken token = GenerateSecurityToken(acc);

				return Ok(new TokenResponse
				{
					AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
					User = acc
				});
				//return Ok(acc);
			}

		}

		private bool CheckPassword(string password, User user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}



		private JwtSecurityToken GenerateSecurityToken(User acc)
		{
			string role = acc.RoleId.ToString();

			var authClaims = new List<Claim>
			{
					new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Role, role),
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		[HttpPost("Sendconfirmation")]
		public async Task<IActionResult> SendConfirmationEmail([FromBody] string toEmail)
		{
			string userid = "";
			string confirmationCode = GenerateConfirmationCode(toEmail);
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
			string message = $"Vui lòng nhấp vào liên kết sau để xác nhận thay đổi trạng thái: {GetConfirmationLink("1", confirmationCode)}";

			try
			{
				await SendEmail(toEmail, subject, message);
				return Ok();
			}
			catch (Exception ex)
			{
				// Xử lý lỗi gửi email
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		[HttpPost("postanh")]
		public async Task<IActionResult> PostAnh(int postid, List<IFormFile> files)
		{
			using (var context = new SEP490_HFS_2Context())
			{
				foreach (var file in files)
				{
					var result = await _photoService.AddPhotoAsync(file);
					if (result.Error != null)
						return BadRequest(result.Error.Message);

					var img = new PostImage
					{
						Path = result.SecureUrl.AbsoluteUri,
						PublicId=result.PublicId,
						PostId = postid
					};

					context.PostImages.Add(img);
				}

				context.SaveChanges();
			}

			return Ok();
		}

		[HttpPost("confirmation")]
		public async Task<IActionResult> ConfirmationEmail([FromBody] string code)
		{
			string userId = "1";
			bool check = ValidateConfirmationCode(code, out userId);
			if (check == true)
			{
				return Ok("DA CONFIMATION THANH CONG");
			}
			else
			{
				return Ok("KHONG THANH CONG");
			}
		}

		private string GenerateConfirmationCode(string userId)//tạo ra mã đễ 
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("userId", userId) }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var confirmationCode = tokenHandler.WriteToken(token);

			return confirmationCode;
		}
		private bool ValidateConfirmationCode(string confirmationCode, out string userId)
		{
			 userId = null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

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
				using(var context=new SEP490_HFS_2Context())
				{
					var data = context.Users.Where(s => s.Email == email).FirstOrDefault();
					if (data != null)
					{
						return false;
					}
					data.ConfirmEmail = true;
					context.Users.Update(data);
					context.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		[HttpGet("hihi")]
		public IActionResult ValidateConfirmationCode1(string confirmationCode)
		{
			string userId = null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

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
				userId = jwtToken.Claims.First(c => c.Type == "userId").Value;

				return Ok(userId);
			}
			catch
			{
				return BadRequest();
			}
		}
		public class TokenResponse
		{
			public string AccessToken { get; set; }
			public User User { get; set; }
		}

		private string GetConfirmationLink(string userId, string confirmationCode)
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

		[HttpPost("okdi")]
		public async Task<IActionResult> SendMail(string toEmail, string content)
		{
			try
			{
				string from = "holafoodfpt@gmail.com";
				string pass = "wqsq fqmv iwhu ablr";
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient("smtp.gmail.com");
				Random r = new Random();
				int random = r.Next(1000, 9999);
				mail.To.Add(toEmail);
				mail.From = new MailAddress(from);
				mail.Subject = "PRN221";
				mail.Body = "Code:" + random;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Credentials = new NetworkCredential(from, pass);
				await smtp.SendMailAsync(mail);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}

		}

	}
}




